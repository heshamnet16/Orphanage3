using OrphanageDataModel.RegularData;
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
        private readonly ILogger _logger;

        public FamilyDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService, IFatherDbService fatherDbService
            , IMotherDbService motherDbService, IOrphanDbService orphanDbService, ILogger logger)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _fatherDbService = fatherDbService;
            _motherDbService = motherDbService;
            _orphanDbService = orphanDbService;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.RegularData.Family> AddFamily(OrphanageDataModel.RegularData.Family family)
        {
            _logger.Information($"trying to add new family");
            if (family == null)
            {
                _logger.Error($"the given family parameter is null, null will be returned");
                return null;
            }
            if (family.PrimaryAddress == null)
            {
                _logger.Error($"the given primary address of the given family parameter is null, null will be returned");
                return null;
            }

            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var DbT = orphanageDbc.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        _logger.Information($"ForceAdd option is not activated");
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            _logger.Information($"CheckContactData option is activated, trying to get the equal contact data for the primary address from database");
                            var ret = GetFamiliesByAddress(family.PrimaryAddress, orphanageDbc).FirstOrDefault();
                            if (ret != null)
                            {
                                _logger.Error($"family with id({ret.Id}) has the same address, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(family.GetType(), ret.GetType(), ret.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar contact data to family primary address in the database");
                            }
                            if (family.AlternativeAddress != null)
                            {
                                _logger.Information($"CheckContactData option is activated, trying to get the equal contact data for the alternative address from database");
                                ret = GetFamiliesByAddress(family.AlternativeAddress, orphanageDbc).FirstOrDefault();
                                if (ret != null)
                                {
                                    _logger.Error($"family with id({ret.Id}) has the same address, DuplicatedObjectException will be thrown");
                                    throw new DuplicatedObjectException(family.GetType(), ret.GetType(), ret.Id);
                                }
                                else
                                {
                                    _logger.Information($"didn't found any similar contact data to family alternative address in the database");
                                }
                            }
                        }
                    }

                    var addressPrim = family.PrimaryAddress;
                    var addressAlter = family.AlternativeAddress;
                    var father = family.Father;
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
                    var taskFather = _fatherDbService.AddFather(father, orphanageDbc);
                    family.FatherId = await taskFather;
                    family.Father = null;
                    // set mother
                    var taskMother = _motherDbService.AddMother(mother, orphanageDbc);
                    family.MotherId = await taskMother;
                    family.Mother = null;

                    if (family.Bail != null)
                    {
                        family.Bail = null;
                    }
                    if (family.ActingUser != null)
                    {
                        family.ActingUser = null;
                    }
                    if (family.Orphans != null)
                    {
                        family.Orphans = null;
                    }
                    orphanageDbc.Families.Add(family);
                    if (await orphanageDbc.SaveChangesAsync() > 0)
                    {
                        DbT.Commit();
                        _logger.Information($"family with id({family.Id}) has been successfully added to the database");
                    }
                    else
                    {
                        DbT.Rollback();
                        _logger.Information($"nothing has changed, family has not added, null will be returned");
                        return null;
                    }
                }
            }
            return await GetFamily(family.Id);
        }

        public async Task<bool> DeleteFamily(int Famid)
        {
            _logger.Information($"trying to delete family with id({Famid})");
            if (Famid <= 0)
            {
                _logger.Warning($"the given family id ({Famid}) is not valid, false will be returned");
                return false;
            }
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var allIsOK = false;
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                var dbT = orphanageDbc.Database.BeginTransaction();
                var famToDelete = await orphanageDbc.Families.Where(fam => fam.Id == Famid).FirstOrDefaultAsync();

                if (famToDelete == null)
                {
                    _logger.Error($"family with id ({Famid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new NullReferenceException();
                }

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
                    _logger.Information($"family with id({Famid}) has been successfully deleted from the database");
                    return true;
                }
                else
                {
                    dbT.Rollback();
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }

        private async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> prepareFamiliesList(IEnumerable<OrphanageDataModel.RegularData.Family> familiesList)
        {
            _logger.Information($"trying to prepare families list to return it");
            if (familiesList == null || familiesList.Count() == 0)
            {
                _logger.Information($"there is no family list to return, null will be returned");
                return null;
            }
            IList<OrphanageDataModel.RegularData.Family> returnedFamiliesList = new List<OrphanageDataModel.RegularData.Family>();
            foreach (var family in familiesList)
            {
                OrphanageDataModel.RegularData.Family familyToFill = family;
                familyToFill.OrphansCount = await GetOrphansCount(family.Id);
                _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                _uriGenerator.SetFamilyUris(ref familyToFill);
                returnedFamiliesList.Add(familyToFill);
            }
            _logger.Information($"{returnedFamiliesList.Count} records of families are ready and they will be returned");
            return returnedFamiliesList;
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetExcludedFamilies()
        {
            _logger.Information($"trying to get all excluded families");
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

                return await prepareFamiliesList(families);
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int pageSize, int pageNum)
        {
            _logger.Information($"trying to get families by paging, pageSize({pageSize}) pageNumber({pageNum})");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                _logger.Information($"total page to skip equals ({totalSkiped})");
                int FamiliesCount = await GetFamiliesCount();
                if (FamiliesCount < totalSkiped)
                {
                    totalSkiped = FamiliesCount - pageSize;
                    _logger.Information($"families count is smaller than total skipped families, reset total skipped families to ({totalSkiped})");
                }
                if (totalSkiped < 0)
                {
                    totalSkiped = 0;
                    _logger.Information($"total skipped families is less than zero, reset total skipped families to ({totalSkiped})");
                }
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

                return await prepareFamiliesList(families);
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(IEnumerable<int> familiesIds)
        {
            _logger.Information($"trying to get families with the given Id list");
            if (familiesIds == null || familiesIds.Count() == 0)
            {
                _logger.Information($"the given Id list is null or empty, null will be returned");
                return null;
            }
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

                return await prepareFamiliesList(families);
            }
        }

        public IEnumerable<OrphanageDataModel.RegularData.Family> GetFamiliesByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get family with the similar address");
            if (addressObject == null)
            {
                _logger.Error($"address object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

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
                _logger.Information($"family with id({family.Id}) has the same address");
                yield return family;
            }
        }

        public async Task<int> GetFamiliesCount()
        {
            _logger.Information($"trying to get all families count");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int familiesCount = await _orphanageDBC.Families.AsNoTracking().CountAsync();
                _logger.Information($"orphans count({familiesCount}) will be returned");
                return familiesCount;
            }
        }

        public async Task<OrphanageDataModel.RegularData.Family> GetFamily(int FamId)
        {
            _logger.Information($"trying to get family with id ({FamId})");
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

                if (family == null)
                {
                    _logger.Error($"family with id ({FamId}) has not been found, ObjectNotFoundException will be thrown");
                    return null;
                }

                OrphanageDataModel.RegularData.Family familyToFill = family;
                familyToFill.OrphansCount = await GetOrphansCount(family.Id);
                _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                _uriGenerator.SetFamilyUris(ref familyToFill);
                _logger.Information($"family object with id({FamId}) will be returned");
                return familyToFill;
            }
        }

        public async Task<byte[]> GetFamilyCardPage1(int FamId)
        {
            _logger.Information($"trying to get family with id ({FamId}) family card photo page1");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage1Data }).FirstOrDefaultAsync();

                if (img?.FamilyCardImagePage1Data == null)
                {
                    _logger.Information($"family({FamId}) family card photo page1 is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return family with id({FamId}) family card photo page1, count of bytes are {img?.FamilyCardImagePage1Data.Length}");
                    return img?.FamilyCardImagePage1Data;
                }
            }
        }

        public async Task<byte[]> GetFamilyCardPage2(int FamId)
        {
            _logger.Information($"trying to get family with id ({FamId}) family card photo page2");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage2Data }).FirstOrDefaultAsync();
                if (img?.FamilyCardImagePage2Data == null)
                {
                    _logger.Information($"family with id({FamId}) family card photo page2");
                    return null;
                }
                else
                {
                    _logger.Information($"return family with id({FamId}) family card photo page2, count of bytes are {img?.FamilyCardImagePage2Data.Length}");
                    return img?.FamilyCardImagePage2Data;
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FamId)
        {
            _logger.Information($"trying to get all orphans of the family with id ({FamId})");
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
                if (orphans == null || orphans.Count == 0)
                {
                    _logger.Information($"family with id({FamId}) has no orphans, null will be returned");
                    return null;
                }
                foreach (var orphan in orphans)
                {
                    var orpToFill = orphan;
                    _selfLoopBlocking.BlockOrphanSelfLoop(ref orpToFill);
                    _uriGenerator.SetOrphanUris(ref orpToFill);
                    _logger.Information($"adding orphan with id({orphan.Id}) to the returned List");
                    returnedOrphans.Add(orpToFill);
                }
            }
            _logger.Information($"({returnedOrphans.Count}) records of orphans will be returned");
            return returnedOrphans;
        }

        public async Task<int> GetOrphansCount(int FamId)
        {
            _logger.Information($"trying to get orphans count of the family with id ({FamId})");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Orphans.Where(o => o.FamilyId == FamId).AsNoTracking().CountAsync();
                _logger.Information($"({orphansCount}) is the count of orphans in the family with id({FamId})");
                return orphansCount;
            }
        }

        public Task<bool> IsExist(OrphanageDataModel.RegularData.Family family)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveFamily(OrphanageDataModel.RegularData.Family family)
        {
            _logger.Information($"trying to save family");
            if (family == null)
            {
                _logger.Error($"family parameter is null, false will be returned");
                return false;
            }
            _logger.Information($"trying to save family with id({family.Id})");

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
                _logger.Information($"processing alternative address to the family with id({family.Id})");
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
                _logger.Information($"processing primary address to the family with id({family.Id})");
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
                {
                    _logger.Information($"family with id({family.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return true;
                }
                else
                {
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }

        public async Task SetFamilyColor(int FamId, int? colorValue)
        {
            try
            {
                _logger.Information($"trying to set the color value ({colorValue ?? -1}) to the family with Id({FamId})");
                using (var _orphanageDBC = new OrphanageDBC())
                {
                    _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                    _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                    _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                    var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                    if (family == null)
                    {
                        _logger.Error($"family with id ({FamId}) has not been found, ObjectNotFoundException will be thrown");
                        throw new ObjectNotFoundException();
                    }

                    family.ColorMark = colorValue;

                    if (await _orphanageDBC.SaveChangesAsync() > 0)
                    {
                        _logger.Information($"color value ({colorValue ?? -1}) has been set successfully to the family with id({FamId})");
                    }
                    else
                    {
                        _logger.Warning($"color value ({colorValue ?? -1}) has not been set to the family with id({FamId}), nothing has changed");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error in SetFamilyCardPage1 method.", ex);
            }
        }

        public async Task SetFamilyExclude(int FamId, bool value)
        {
            _logger.Information($"trying to set the isExcluded value ({value.ToString()}) to the family with Id({FamId})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                if (family == null)
                {
                    _logger.Error($"family with id ({FamId}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                family.IsExcluded = value;

                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"isExcluded value ({value}) has been set successfully to the family with id({FamId})");
                }
                else
                {
                    _logger.Warning($"isExcluded value ({value}) has not been set to the family with id({FamId}), nothing has changed");
                }
            }
        }

        public async Task<bool> SetFamilyCardPage1(int FamId, byte[] data)
        {
            _logger.Information($"trying to set family with id ({FamId}) FamilyCard Page1 Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                if (family == null)
                {
                    _logger.Error($"family with id ({FamId}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                family.FamilyCardImagePage1Data = data;

                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new FamilyCard Page1 Photo has been set successfully to the family with id ({FamId}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new FamilyCard Page1 Photo to the family with id ({FamId}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetFamilyCardPage2(int FamId, byte[] data)
        {
            _logger.Information($"trying to set family with id ({FamId}) FamilyCard Page2 Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var family = await _orphanageDBC.Families.Where(f => f.Id == FamId).FirstOrDefaultAsync();

                if (family == null)
                {
                    _logger.Error($"family with id ({FamId}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                family.FamilyCardImagePage2Data = data;

                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new FamilyCard Page2 Photo has been set successfully to the family with id ({FamId}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new FamilyCard Page2 Photo to the family with id ({FamId}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> BailFamilies(int BailId, IList<int> familiesIds)
        {
            bool ret = true;
            _logger.Information($"trying to set Bail with id({BailId}) to the families with the given Id list");
            _logger.Information($"trying to get families with the given Id list");
            if (familiesIds == null || familiesIds.Count == 0)
            {
                _logger.Information($"the given Id list is null or empty, false will be returned");
                return false;
            }
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var families = await _orphanageDBC.Families
                    .Where(o => familiesIds.Contains(o.Id))
                    .ToListAsync();

                if (families == null || families.Count == 0)
                {
                    _logger.Warning($"there is no families founded in the given ids list, false will be returned");
                    return false;
                }
                OrphanageDataModel.FinancialData.Bail bail = null;
                if (BailId > 0)
                {
                    bail = await _orphanageDBC.Bails.AsNoTracking().FirstOrDefaultAsync(b => b.Id == BailId);
                    if (bail == null)
                    {
                        _logger.Error($"bail with id ({BailId}) has not been found, ObjectNotFoundException will be thrown");
                        throw new ObjectNotFoundException();
                    }
                }
                foreach (var fam in families)
                {
                    if (BailId > 0)
                    {
                        _logger.Information($"trying to set value ({BailId}) to bailId property for the family with the id ({fam.Id})");
                        fam.IsBailed = true;
                        fam.BailId = BailId;
                    }
                    else
                    {
                        _logger.Information($"trying to set bailId property to NULL for the family with the id ({fam.Id})");
                        fam.IsBailed = false;
                        fam.BailId = null;
                    }
                    if (await _orphanageDBC.SaveChangesAsync() > 0)
                    {
                        _logger.Information($"bailId property has been set successfully to the value ({BailId}) for the family with the id ({fam.Id})");
                    }
                    else
                    {
                        _logger.Warning($"bailId property has not been set to the value ({BailId}) for the family with the id ({fam.Id})");
                        ret = false;
                    }
                }
            }
            return ret;
        }
    }
}