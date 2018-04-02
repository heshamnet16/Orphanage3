﻿using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class FamilyDbService : IFamilyDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;
        private readonly IFatherDbService _fatherDbService;
        private readonly IMotherDbService _motherDbService;
        private readonly IOrphanDbService _orphanDbService;

        public FamilyDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService, IFatherDbService fatherDbService
            , IMotherDbService motherDbService, IOrphanDbService orphanDbService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _fatherDbService = fatherDbService;
            _motherDbService = motherDbService;
            _orphanDbService = orphanDbService;
        }

        public async Task<OrphanageDataModel.RegularData.Family> AddFamily(OrphanageDataModel.RegularData.Family family)
        {
            if (family == null) throw new NullReferenceException();
            if (family.PrimaryAddress == null) throw new NullReferenceException();

            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var DbT = orphanageDbc.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            var ret = GetFamiliesByAddress(family.PrimaryAddress, orphanageDbc).FirstOrDefault();
                            if (ret != null)
                            {
                                throw new DuplicatedObjectException(family.GetType(), ret.GetType(), ret.Id);
                            }
                            if (family.AlternativeAddress != null)
                            {
                                ret = GetFamiliesByAddress(family.AlternativeAddress, orphanageDbc).FirstOrDefault();
                                if (ret != null)
                                {
                                    throw new DuplicatedObjectException(family.GetType(), ret.GetType(), ret.Id);
                                }
                            }
                        }
                    }

                    var addressPrim = family.PrimaryAddress;
                    var addressAlter = family.AlternativeAddress;
                    var fatherName = family.Father.Name;
                    var father = family.Father;
                    var motherName = family.Mother.Name;
                    var motherAddress = family.Mother.Address;
                    var mother = family.Mother;
                    var taskPrimAddress = _regularDataService.AddAddress(addressPrim, orphanageDbc);
                    Task<int> taskAlterAddress = null;
                    family.AddressId = await taskPrimAddress;
                    family.PrimaryAddress = addressPrim;
                    if (family.AlternativeAddress != null)
                    {
                        taskAlterAddress = _regularDataService.AddAddress(addressAlter, orphanageDbc);
                    }
                    if (family.Orphans != null && family.Orphans.Count > 0)
                    {
                        family.Orphans = null;
                    }
                    if (taskAlterAddress != null)
                    {
                        family.AlternativeAddressId = await taskAlterAddress;
                        family.AlternativeAddress = addressAlter;
                    }
                    // set father
                    var taskFatherName = _regularDataService.AddName(fatherName, orphanageDbc);
                    father.NameId = await taskFatherName;
                    father.Name = fatherName;
                    var taskFather = _fatherDbService.AddFather(father, orphanageDbc);
                    family.FatherId = await taskFather;
                    _uriGenerator.SetFatherUris(ref father);
                    family.Father = father;
                    // set mother
                    var taskMotherName = _regularDataService.AddName(motherName, orphanageDbc);
                    mother.NameId = await taskMotherName;
                    mother.Name = motherName;
                    var taskMotherAddress = _regularDataService.AddAddress(motherAddress, orphanageDbc);
                    mother.AddressId = await taskMotherAddress;
                    mother.Address = motherAddress;
                    var taskMother = _motherDbService.AddMother(mother, orphanageDbc);
                    family.MotherId = await taskMother;
                    _uriGenerator.SetMotherUris(ref mother);
                    family.Mother = mother;

                    orphanageDbc.Families.Add(family);
                    if (await orphanageDbc.SaveChangesAsync() > 0)
                    {
                        DbT.Commit();
                        _uriGenerator.SetFamilyUris(ref family);
                        return family;
                    }
                    else
                    {
                        DbT.Rollback();
                        return null;
                    }
                }
            }
        }

        public async Task<bool> DeleteFamily(int Famid)
        {
            if (Famid <= 0) return false;
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var allIsOK = false;
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                var dbT = orphanageDbc.Database.BeginTransaction();
                var famToDelete = await orphanageDbc.Families.Where(fam => fam.Id == Famid).FirstOrDefaultAsync();

                if (famToDelete == null) throw new NullReferenceException();

                var fatherID = famToDelete.FatherId;
                var motherID = famToDelete.MotherId;
                var familyA = famToDelete.PrimaryAddress;
                var familyAA = famToDelete.AlternativeAddress;
                var orphans = famToDelete.Orphans;
                if (orphans != null && orphans.Count > 0)
                {
                    int orphansCount = orphans.Count;
                    var orphansList = orphans.ToList();
                    for (int orpIndex = 0; orpIndex < orphansCount; orpIndex++)
                    {
                        var orp = orphansList[orpIndex];
                        allIsOK = await _orphanDbService.DeleteOrphan(orp.Id, orphanageDbc);
                        if (!allIsOK)
                        {
                            dbT.Rollback();
                            return false;
                        }
                    }
                }
                orphanageDbc.Families.Remove(famToDelete);
                allIsOK = await orphanageDbc.SaveChangesAsync() > 0 ? true : false;
                if (!allIsOK)
                {
                    dbT.Rollback();
                    return false;
                }
                allIsOK = await _fatherDbService.DeleteFather(fatherID, orphanageDbc);
                if (!allIsOK)
                {
                    dbT.Rollback();
                    return false;
                }
                allIsOK = await _motherDbService.DeleteMother(motherID, orphanageDbc);
                orphanageDbc.Addresses.Remove(familyA);
                if (familyAA != null) orphanageDbc.Addresses.Remove(familyAA);
                if (await orphanageDbc.SaveChangesAsync() > 0 && allIsOK)
                {
                    dbT.Commit();
                    return true;
                }
                else
                {
                    dbT.Rollback();
                    return false;
                }
            }
        }

        private IEnumerable<OrphanageDataModel.RegularData.Family> prepareFamiliesList(IEnumerable<OrphanageDataModel.RegularData.Family> familiesList)
        {
            if (familiesList == null || familiesList.Count() == 0) return null;
            IList<OrphanageDataModel.RegularData.Family> returnedFamiliesList = new List<OrphanageDataModel.RegularData.Family>();
            foreach (var family in familiesList)
            {
                OrphanageDataModel.RegularData.Family familyToFill = family;
                _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                _uriGenerator.SetFamilyUris(ref familyToFill);
                returnedFamiliesList.Add(familyToFill);
            }
            return returnedFamiliesList;
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetExcludedFamilies()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Father.Name)
                    .Include(f => f.Mother)
                    .Include(f => f.Mother.Name)
                    .Include(f => f.Mother.Address)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .Where(f => f.IsExcluded == true)
                    .ToListAsync();

                return prepareFamiliesList(families);
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int pageSize, int pageNum)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FamiliesCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FamiliesCount < totalSkiped)
                {
                    totalSkiped = FamiliesCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Father.Name)
                    .Include(f => f.Mother)
                    .Include(f => f.Mother.Name)
                    .Include(f => f.Mother.Address)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .ToListAsync();

                return prepareFamiliesList(families);
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(IEnumerable<int> familiesIds)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .Where(f => familiesIds.Contains(f.Id))
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Father.Name)
                    .Include(f => f.Mother)
                    .Include(f => f.Mother.Name)
                    .Include(f => f.Mother.Address)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .ToListAsync();

                return prepareFamiliesList(families);
            }
        }

        public IEnumerable<OrphanageDataModel.RegularData.Family> GetFamiliesByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (addressObject == null) throw new NullReferenceException();

            var families = orphanageDbCNo.Families
            .Include(m => m.PrimaryAddress)
            .Include(f => f.AlternativeAddress)
            .ToArray();

            var Foundedfamilies = families.Where(n =>
            n.PrimaryAddress.Equals(addressObject)
            ||
            (
            (n.AlternativeAddress != null) && n.AlternativeAddress.Equals(addressObject)
            ));

            if (Foundedfamilies == null) yield return null;

            foreach (var family in Foundedfamilies)
            {
                yield return family;
            }
        }

        public async Task<int> GetFamiliesCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int familiesCount = await _orphanageDBC.Families.AsNoTracking().CountAsync();
                return familiesCount;
            }
        }

        public async Task<OrphanageDataModel.RegularData.Family> GetFamily(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var family = await _orphanageDBC.Families.AsNoTracking()
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Father.Name)
                    .Include(f => f.Mother)
                    .Include(f => f.Mother.Name)
                    .Include(f => f.Mother.Address)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .FirstOrDefaultAsync(f => f.Id == FamId);

                if (family == null) return null;

                OrphanageDataModel.RegularData.Family familyToFill = family;
                _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                _uriGenerator.SetFamilyUris(ref familyToFill);
                return familyToFill;
            }
        }

        public async Task<byte[]> GetFamilyCardPage1(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage1Data }).FirstOrDefaultAsync();
                return img?.FamilyCardImagePage1Data;
            }
        }

        public async Task<byte[]> GetFamilyCardPage2(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage2Data }).FirstOrDefaultAsync();
                return img?.FamilyCardImagePage2Data;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FamId)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     where orp.FamilyId == FamId
                                     select orp)
                                     .Include(o => o.Education)
                                     .Include(o => o.Name)
                                     .Include(o => o.Caregiver.Name)
                                     .Include(o => o.Caregiver.Address)
                                     .Include(o => o.Family.Father.Name)
                                     .Include(o => o.Family.Mother.Name)
                                     .Include(o => o.Family.PrimaryAddress)
                                     .Include(o => o.Family.AlternativeAddress)
                                     .Include(o => o.Guarantor.Name)
                                     .Include(o => o.Bail)
                                     .Include(o => o.HealthStatus)
                              .ToListAsync();

                foreach (var orphan in orphans)
                {
                    var orpToFill = orphan;
                    _selfLoopBlocking.BlockOrphanSelfLoop(ref orpToFill);
                    _uriGenerator.SetOrphanUris(ref orpToFill);
                    returnedOrphans.Add(orpToFill);
                }
            }
            return returnedOrphans;
        }

        public async Task<int> GetOrphansCount(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int familiesCount = await _orphanageDBC.Orphans.Where(o => o.FamilyId == FamId).AsNoTracking().CountAsync();
                return familiesCount;
            }
        }

        public Task<bool> IsExist(OrphanageDataModel.RegularData.Family family)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveFamily(OrphanageDataModel.RegularData.Family family)
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                orphanageDbc.Configuration.AutoDetectChangesEnabled = true;
                var savedFamily = await orphanageDbc.Families.
                    Include(f => f.AlternativeAddress).
                    Include(f => f.PrimaryAddress)
                    .Where(fam => fam.Id == family.Id).FirstOrDefaultAsync();
                if (family.AlternativeAddress != null)
                {
                    if (savedFamily.AlternativeAddress == null)
                    {
                        var addressID = await _regularDataService.AddAddress(family.AlternativeAddress, orphanageDbc);
                        savedFamily.AlternativeAddressId = addressID;
                    }
                    else
                    {
                        ret += await _regularDataService.SaveAddress(family.AlternativeAddress, orphanageDbc);
                    }
                }
                else
                {
                    if (savedFamily.AlternativeAddress != null)
                    {
                        int alAdd = savedFamily.AlternativeAddressId.Value;
                        savedFamily.AlternativeAddressId = null;
                        await orphanageDbc.SaveChangesAsync();
                        await _regularDataService.DeleteAddress(alAdd, orphanageDbc);
                    }
                }
                if (family.PrimaryAddress != null)
                {
                    if (savedFamily.PrimaryAddress == null)
                    {
                        var addressID = await _regularDataService.AddAddress(family.PrimaryAddress, orphanageDbc);
                        savedFamily.AddressId = addressID;
                        ret++;
                    }
                    else
                    {
                        ret += await _regularDataService.SaveAddress(family.PrimaryAddress, orphanageDbc);
                    }
                }
                else
                {
                    if (savedFamily.PrimaryAddress != null)
                    {
                        int Add = savedFamily.AddressId.Value;
                        savedFamily.AddressId = null;
                        await orphanageDbc.SaveChangesAsync();
                        await _regularDataService.DeleteAddress(Add, orphanageDbc);
                    }
                }
                savedFamily.FatherId = family.FatherId;
                ret += await _fatherDbService.SaveFather(family.Father);
                savedFamily.MotherId = family.MotherId;
                ret += await _motherDbService.SaveMother(family.Mother);
                savedFamily.BailId = family.BailId;
                savedFamily.ColorMark = family.ColorMark;
                savedFamily.FinncialStatus = family.FinncialStatus;
                savedFamily.IsBailed = family.IsBailed;
                savedFamily.IsExcluded = family.IsExcluded;
                savedFamily.IsTheyRefugees = family.IsTheyRefugees;
                savedFamily.Note = family.Note;
                savedFamily.ResidenceStatus = family.ResidenceStatus;
                savedFamily.ResidenceType = family.ResidenceType;
                ret += await orphanageDbc.SaveChangesAsync();
                if (ret > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task SetFamilyColor(int FamId, int? colorValue)
        {
            try
            {
                using (var _orphanageDBC = new OrphanageDBC())
                {
                    _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                    _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                    _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                    var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                    if (family == null)
                        return;

                    family.ColorMark = colorValue;

                    await _orphanageDBC.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error in SetFamilyCardPage1 method.", ex);
            }
        }

        public async Task SetFamilyExclude(int FamId, bool value)
        {
            try
            {
                using (var _orphanageDBC = new OrphanageDBC())
                {
                    _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                    _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                    _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                    var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                    if (family == null)
                        return;

                    family.IsExcluded = value;

                    await _orphanageDBC.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error in SetFamilyCardPage1 method.", ex);
            }
        }

        public async Task SetFamilyCardPage1(int FamId, byte[] data)
        {
            try
            {
                using (var _orphanageDBC = new OrphanageDBC())
                {
                    _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                    _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                    _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                    var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                    if (family == null)
                        return;

                    family.FamilyCardImagePage1Data = data;

                    await _orphanageDBC.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error in SetFamilyCardPage1 method.", ex);
            }
        }

        public async Task SetFamilyCardPage2(int FamId, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                if (family == null)
                    return;

                family.FamilyCardImagePage2Data = data;

                await _orphanageDBC.SaveChangesAsync();
            }
        }
    }
}