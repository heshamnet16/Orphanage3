using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class CaregiverDbService : ICaregiverDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;

        public CaregiverDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
        }

        public async Task<int> AddCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            if (caregiver == null) throw new NullReferenceException();
            if (caregiver.Name == null) throw new NullReferenceException();
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        if (Properties.Settings.Default.CheckName)
                        {
                            var retCaregivers = GetCaregiversByName(caregiver.Name, orphanageDBC).FirstOrDefault();
                            if (retCaregivers != null)
                            {
                                throw new DuplicatedObjectException(caregiver.GetType(), retCaregivers.GetType(), retCaregivers.Id);
                            }
                        }
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            var retCaregivers = GetCaregiversByAddress(caregiver.Address, orphanageDBC).FirstOrDefault();
                            if (retCaregivers != null)
                            {
                                throw new DuplicatedObjectException(caregiver.GetType(), retCaregivers.GetType(), retCaregivers.Id);
                            }
                        }
                    }
                    var nameId = await _regularDataService.AddName(caregiver.Name, orphanageDBC);
                    if (nameId == -1)
                    {
                        Dbt.Rollback();
                        return -1;
                    }
                    caregiver.NameId = nameId;
                    if (caregiver.Address != null)
                    {
                        var addressId = await _regularDataService.AddAddress(caregiver.Address, orphanageDBC);
                        if (addressId == -1)
                        {
                            Dbt.Rollback();
                            return -1;
                        }
                        caregiver.AddressId = addressId;
                    }
                    if (caregiver.Orphans != null || caregiver.Orphans.Count > 0) caregiver.Orphans = null;
                    orphanageDBC.Caregivers.Add(caregiver);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        return caregiver.Id;
                    }
                    else
                    {
                        Dbt.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task<bool> DeleteCaregiver(int Cid)
        {
            if (Cid <= 0) throw new NullReferenceException();
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    var caregiver = await orphanageDb.Caregivers.Where(c => c.Id == Cid)
                        .Include(c => c.Name)
                        .Include(c => c.Address)
                        .Include(c => c.Orphans)
                        .FirstOrDefaultAsync();

                    if (caregiver == null) throw new ObjectNotFoundException();
                    if (caregiver.Orphans.Count > 0)
                    {
                        //the caregiver has another orphans
                        throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Caregiver), typeof(OrphanageDataModel.Persons.Orphan));
                    }
                    var caregiverName = caregiver.Name;
                    var caregiverAddress = caregiver.Address;
                    orphanageDb.Caregivers.Remove(caregiver);
                    await orphanageDb.SaveChangesAsync();
                    orphanageDb.Names.Remove(caregiverName);
                    orphanageDb.Addresses.Remove(caregiverAddress);
                    if (await orphanageDb.SaveChangesAsync() > 1)
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
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> GetCaregiver(int Cid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var caregiver = await dbContext.Caregivers.AsNoTracking()
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Include(c => c.ActingUser.Name)
                    .FirstOrDefaultAsync(c => c.Id == Cid);

                if (caregiver == null) throw new ObjectNotFoundException();
                _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiver);
                _uriGenerator.SetCaregiverUris(ref caregiver);
                caregiver.Address = caregiver.Address.Clean();
                return caregiver;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Caregiver> caregiversList = new List<OrphanageDataModel.Persons.Caregiver>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking().CountAsync();
                if (caregiversCount < totalSkiped)
                {
                    totalSkiped = caregiversCount - pageSize;
                }
                var caregivers = await _orphanageDBC.Caregivers.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Include(c=>c.ActingUser.Name)
                    .ToListAsync();

                foreach (var caregiver in caregivers)
                {
                    OrphanageDataModel.Persons.Caregiver caregiverToFill = caregiver;
                    _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverToFill);
                    caregiverToFill.Address = caregiverToFill.Address.Clean();
                    caregiversList.Add(caregiverToFill);
                }
            }
            return caregiversList;
        }

        public IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (addressObject == null) throw new NullReferenceException();

            var caregivers = orphanageDbCNo.Caregivers
            .Include(m => m.Address)
            .ToArray();

            var FoundedCaregivers = caregivers.Where(n => n.Address.Equals(addressObject));

            foreach (var caregiver in FoundedCaregivers)
            {
                caregiver.Address = caregiver.Address.Clean();
                yield return caregiver;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (nameObject == null) throw new NullReferenceException();

            var caregivers = orphanageDbCNo.Caregivers
            .Include(m => m.Name)
            .Include(m => m.Address)
            .ToArray();

            var FoundedCaregivers = caregivers.Where(n =>
            n.Name.Equals(nameObject));

            if (FoundedCaregivers == null) yield return null;

            foreach (var caregiver in FoundedCaregivers)
            {
                caregiver.Address = caregiver.Address.Clean();
                yield return caregiver;
            }
        }

        public async Task<int> GetCaregiversCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking().CountAsync();
                return caregiversCount;
            }
        }

        public async Task<byte[]> GetIdentityCardBack(int Cid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Caregivers.AsNoTracking().Where(c => c.Id == Cid).Select(c => new { c.IdentityCardPhotoBackData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoBackData;
            }
        }

        public async Task<byte[]> GetIdentityCardFace(int Cid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Caregivers.AsNoTracking().Where(c => c.Id == Cid).Select(c => new { c.IdentityCardPhotoFaceData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoFaceData;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Cid)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     where orp.CaregiverId == Cid
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

        public async Task<bool> SaveCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            if (caregiver == null) throw new NullReferenceException();
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;

                var orginalCaregiver = await orphanageDc.Caregivers.
                    Include(m => m.Address).
                    Include(c => c.Name).
                    FirstOrDefaultAsync(m => m.Id == caregiver.Id);

                if (orginalCaregiver == null) throw new ObjectNotFoundException();

                if (caregiver.Address != null)
                    if (orginalCaregiver.Address != null)
                        //edit existing caregiver address
                        ret += await _regularDataService.SaveAddress(caregiver.Address, orphanageDc);
                    else
                    {
                        //create new address for the caregiver
                        var addressId = await _regularDataService.AddAddress(caregiver.Address, orphanageDc);
                        orginalCaregiver.AddressId = addressId;
                        ret++;
                    }
                else
                    if (orginalCaregiver.Address != null)
                {
                    //delete existing caregiver address
                    int alAdd = orginalCaregiver.AddressId.Value;
                    orginalCaregiver.AddressId = null;
                    await orphanageDc.SaveChangesAsync();
                    await _regularDataService.DeleteAddress(alAdd, orphanageDc);
                }

                ret += await _regularDataService.SaveName(caregiver.Name, orphanageDc);
                orginalCaregiver.IdentityCardId = caregiver.IdentityCardId;
                orginalCaregiver.ColorMark = caregiver.ColorMark;
                orginalCaregiver.Income = caregiver.Income;
                orginalCaregiver.Jop = caregiver.Jop;
                orginalCaregiver.Note = caregiver.Note;
                ret += await orphanageDc.SaveChangesAsync();
                return ret > 0 ? true : false;
            }
        }

        public async Task SetIdentityCardBack(int Cid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var caregiver = await _orphanageDBC.Caregivers.FirstOrDefaultAsync(m => m.Id == Cid);

                if (caregiver == null)
                    throw new ObjectNotFoundException();

                caregiver.IdentityCardPhotoBackData = data;

                await _orphanageDBC.SaveChangesAsync();
            }
        }

        public async Task SetIdentityCardFace(int Cid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var caregiver = await _orphanageDBC.Caregivers.FirstOrDefaultAsync(m => m.Id == Cid);

                if (caregiver == null)
                    throw new ObjectNotFoundException();

                caregiver.IdentityCardPhotoFaceData = data;

                await _orphanageDBC.SaveChangesAsync();
            }
        }
    }
}