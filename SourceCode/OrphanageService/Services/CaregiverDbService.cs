using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using OrphanageV3.Extensions;
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
        private readonly ILogger _logger;

        public CaregiverDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService, ILogger logger)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> AddCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            _logger.Information($"Trying to add new caregiver");
            if (caregiver == null)
            {
                _logger.Error($"the parameter object caregiverToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (caregiver.Name == null)
            {
                _logger.Error($"the Name object of the parameter object caregiverToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        _logger.Information($"ForceAdd option is not activated");
                        if (Properties.Settings.Default.CheckName)
                        {
                            _logger.Information($"CheckName option is activated, trying to get the equal names from database");
                            var retCaregivers = GetCaregiversByName(caregiver.Name, orphanageDBC).FirstOrDefault();
                            if (retCaregivers != null)
                            {
                                _logger.Error($"caregiver with id({retCaregivers.Id}) has the same name, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(caregiver.GetType(), retCaregivers.GetType(), retCaregivers.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar names to ({caregiver.Name.FullName()}) in the database");
                            }
                        }
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            _logger.Information($"CheckContactData option is activated, trying to get the equal contact data for the caregiver address from database");
                            var retCaregivers = GetCaregiversByAddress(caregiver.Address, orphanageDBC).FirstOrDefault();
                            if (retCaregivers != null)
                            {
                                _logger.Error($"caregiver with id({retCaregivers.Id}) has the same address, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(caregiver.GetType(), retCaregivers.GetType(), retCaregivers.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar contact data to caregiver address in the database");
                            }
                        }
                    }
                    var nameId = await _regularDataService.AddName(caregiver.Name, orphanageDBC);
                    if (nameId == -1)
                    {
                        Dbt.Rollback();
                        _logger.Warning($"Name object has not been added, nothing will be added, null will be returned");
                        return null;
                    }
                    caregiver.NameId = nameId;
                    if (caregiver.Address != null)
                    {
                        var addressId = await _regularDataService.AddAddress(caregiver.Address, orphanageDBC);
                        if (addressId == -1)
                        {
                            Dbt.Rollback();
                            _logger.Warning($"Address object has not been added, nothing will be added, null will be returned");
                            return null;
                        }
                        caregiver.AddressId = addressId;
                    }
                    if (caregiver.Orphans != null && caregiver.Orphans.Count > 0) caregiver.Orphans = null;
                    orphanageDBC.Caregivers.Add(caregiver);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        _logger.Information($"new caregiver object with id {caregiver.Id} has been added");
                        _uriGenerator.SetCaregiverUris(ref caregiver);
                        _logger.Information($"the caregiver object with id {caregiver.Id}  will be returned");
                        return caregiver;
                    }
                    else
                    {
                        Dbt.Rollback();
                        _logger.Warning($"something went wrong, nothing was added, null will be returned");
                        return null;
                    }
                }
            }
        }

        public async Task<bool> DeleteCaregiver(int Cid)
        {
            _logger.Information($"trying to delete Caregiver with id({Cid})");
            if (Cid <= 0)
            {
                _logger.Error($"the integer parameter Caregiver ID is less than zero, false will be returned");
                throw new NullReferenceException();
            }
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    int ret = 0;
                    var caregiver = await orphanageDb.Caregivers.Where(c => c.Id == Cid)
                        .Include(c => c.Name)
                        .Include(c => c.Address)
                        .Include(c => c.Orphans)
                        .FirstOrDefaultAsync();

                    if (caregiver == null)
                    {
                        _logger.Error($"the original Caregiver object with id {Cid} object is not founded, ObjectNotFoundException will be thrown");
                        throw new ObjectNotFoundException();
                    }
                    if (caregiver.Orphans.Count > 0)
                    {
                        //the caregiver has another orphans
                        _logger.Error($"the Caregiver object with id {Cid} has not null foreign key on Orphans table, HasForeignKeyException will be thrown");
                        throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Caregiver), typeof(OrphanageDataModel.Persons.Orphan));
                    }
                    var caregiverName = caregiver.Name;
                    var caregiverAddress = caregiver.Address;
                    orphanageDb.Caregivers.Remove(caregiver);
                    ret += await orphanageDb.SaveChangesAsync();
                    ret += await _regularDataService.DeleteName(caregiverName.Id, orphanageDb) ? 1 : 0;
                    if (caregiverAddress != null)
                        ret += await _regularDataService.DeleteAddress(caregiverAddress.Id, orphanageDb) ? 1 : 0;
                    if (ret > 0)
                    {
                        dbT.Commit();
                        _logger.Information($"Caregiver with id({Cid}) has been successfully deleted from the database");
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
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> GetCaregiver(int Cid)
        {
            _logger.Information($"Trying to get caregiver with id {Cid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var caregiver = await dbContext.Caregivers.AsNoTracking()
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Include(c => c.ActingUser.Name)
                    .FirstOrDefaultAsync(c => c.Id == Cid);

                if (caregiver == null)
                {
                    _logger.Warning($"caregiver with id{Cid} cannot be found null is returned");
                    return null;
                }
                if (caregiver == null) throw new ObjectNotFoundException();
                _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiver);
                _uriGenerator.SetCaregiverUris(ref caregiver);
                caregiver.Address = caregiver.Address.Clean();
                _logger.Information($"returned caregiver with id {Cid}");
                return caregiver;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int pageSize, int pageNum)
        {
            _logger.Information($"Trying to get Caregiver with pageSize {pageSize} and pageNumber {pageNum}");
            IList<OrphanageDataModel.Persons.Caregiver> caregiversList = new List<OrphanageDataModel.Persons.Caregiver>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking().CountAsync();
                if (caregiversCount < totalSkiped)
                {
                    _logger.Warning($"Total skipped Caregivers({totalSkiped}) are more than the count of all Caregivers ({caregiversCount})");
                    totalSkiped = caregiversCount - pageSize;
                }
                if (totalSkiped < 0)
                {
                    _logger.Warning($"Total skipped Caregivers({totalSkiped}) are less than zero");
                    totalSkiped = 0;
                }
                var caregivers = await _orphanageDBC.Caregivers.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Include(c => c.ActingUser.Name)
                    .ToListAsync();

                if (caregivers != null && caregivers.Count > 0)
                {
                    foreach (var caregiver in caregivers)
                    {
                        OrphanageDataModel.Persons.Caregiver caregiverToFill = caregiver;
                        _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiverToFill);
                        _uriGenerator.SetCaregiverUris(ref caregiverToFill);
                        caregiverToFill.Address = caregiverToFill.Address.Clean();
                        caregiversList.Add(caregiverToFill);
                    }
                }
                else
                {
                    _logger.Warning($"the returned caregivers are null, empty list will be returned");
                }
            }
            return caregiversList;
        }

        public IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get Caregiver with the similar address");

            if (addressObject == null)
            {
                _logger.Error($"address object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var caregivers = orphanageDbCNo.Caregivers
            .Include(m => m.Address)
            .ToArray();

            var FoundedCaregivers = caregivers.Where(n => n.Address.Equals(addressObject));

            foreach (var caregiver in FoundedCaregivers)
            {
                _logger.Information($"Caregiver with id({caregiver.Id}) has the same address");
                caregiver.Address = caregiver.Address.Clean();
                yield return caregiver;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get caregiver with the similar name");

            if (nameObject == null)
            {
                _logger.Error($"name object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var caregivers = orphanageDbCNo.Caregivers
            .Include(m => m.Name)
            .Include(m => m.Address)
            .ToArray();

            var FoundedCaregivers = caregivers.Where(n =>
            n.Name.Equals(nameObject));

            if (FoundedCaregivers == null) yield return null;

            foreach (var caregiver in FoundedCaregivers)
            {
                _logger.Information($"caregiver with id({caregiver.Id}) has the same Name as ({nameObject.FullName()})");
                yield return caregiver;
            }
        }

        public async Task<int> GetCaregiversCount()
        {
            _logger.Information($"Trying to get caregivers count");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking().CountAsync();
                _logger.Information($"all caregivers count ({caregiversCount}) as integer value will be returned");
                return caregiversCount;
            }
        }

        public async Task<byte[]> GetIdentityCardBack(int Cid)
        {
            _logger.Information($"trying to get caregiver with id ({Cid}) Identity Card Back photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Caregivers.AsNoTracking().Where(c => c.Id == Cid).Select(c => new { c.IdentityCardPhotoBackData }).FirstOrDefaultAsync();
                if (img?.IdentityCardPhotoBackData == null)
                {
                    _logger.Information($"caregiver({Cid}) Identity Card Back photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return caregiver({Cid}) Identity Card Back photo, count of bytes are {img?.IdentityCardPhotoBackData.Length}");
                    return img?.IdentityCardPhotoBackData;
                }
            }
        }

        public async Task<byte[]> GetIdentityCardFace(int Cid)
        {
            _logger.Information($"trying to get caregiver with id ({Cid}) Identity Card face photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Caregivers.AsNoTracking().Where(c => c.Id == Cid).Select(c => new { c.IdentityCardPhotoFaceData }).FirstOrDefaultAsync();
                if (img?.IdentityCardPhotoFaceData == null)
                {
                    _logger.Information($"caregiver({Cid}) Identity Card face photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return caregiver({Cid}) Identity Card face photo, count of bytes are {img?.IdentityCardPhotoFaceData.Length}");
                    return img?.IdentityCardPhotoFaceData;
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Cid)
        {
            _logger.Information($"trying to get all orphans of the caregiver with id ({Cid})");
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

                if (orphans == null || orphans.Count == 0)
                {
                    _logger.Information($"caregiver with id({Cid}) has no orphans, null will be returned");
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

        public async Task<bool> SaveCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            _logger.Information($"Trying to save caregiver");
            if (caregiver == null)
            {
                _logger.Error($"the parameter object caregiver is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (caregiver.NameId <= 0)
            {
                _logger.Error($"the NameID of the parameter object caregiver equals {caregiver.NameId}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
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

                if (orginalCaregiver == null)
                {
                    _logger.Error($"the original caregiver object with id {caregiver.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                _logger.Information($"processing the address object of the caregiver with id({caregiver.Id})");
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
                _logger.Information($"processing the name object of the caregiver with id({caregiver.Id})");
                ret += await _regularDataService.SaveName(caregiver.Name, orphanageDc);
                orginalCaregiver.IdentityCardId = caregiver.IdentityCardId;
                orginalCaregiver.ColorMark = caregiver.ColorMark;
                orginalCaregiver.Income = caregiver.Income;
                orginalCaregiver.Jop = caregiver.Jop;
                orginalCaregiver.Note = caregiver.Note;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"caregiver with id({caregiver.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return true;
                }
                else
                {
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }

        public async Task SetCaregiverColor(int Fid, int? value)
        {
            _logger.Information($"trying to set the color value ({value ?? -1}) to the caregiver with Id({Fid})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var caregiver = await _orphanageDBC.Caregivers.FirstOrDefaultAsync(m => m.Id == Fid);

                if (caregiver == null)
                {
                    _logger.Error($"caregiver with id ({Fid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                caregiver.ColorMark = value;

                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"color value ({value ?? -1}) has been set successfully to the caregiver with id({Fid})");
                }
                else
                {
                    _logger.Warning($"color value ({value ?? -1}) has not been set to the caregiver with id({Fid}), nothing has changed");
                }
            }
        }

        public async Task SetIdentityCardBack(int Cid, byte[] data)
        {
            _logger.Information($"trying to set caregiver with id ({Cid}) Identity Card Back Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var caregiver = await _orphanageDBC.Caregivers.FirstOrDefaultAsync(m => m.Id == Cid);

                if (caregiver == null)
                {
                    _logger.Error($"caregiver with id ({Cid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                caregiver.IdentityCardPhotoBackData = data;

                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new Identity Card Back Photo has been set successfully to the caregiver with id ({Cid}), true will be returned");
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new Identity Card Back Photo to the caregiver with id ({Cid}), false will be returned");
                }
            }
        }

        public async Task SetIdentityCardFace(int Cid, byte[] data)
        {
            _logger.Information($"trying to set caregiver with id ({Cid}) Identity Card Face Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var caregiver = await _orphanageDBC.Caregivers.FirstOrDefaultAsync(m => m.Id == Cid);

                if (caregiver == null)
                {
                    _logger.Error($"caregiver with id ({Cid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                caregiver.IdentityCardPhotoFaceData = data;

                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new Identity Card Face Photo has been set successfully to the caregiver with id ({Cid}), true will be returned");
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new Identity Card Face Photo to the caregiver with id ({Cid}), false will be returned");
                }
            }
        }
    }
}