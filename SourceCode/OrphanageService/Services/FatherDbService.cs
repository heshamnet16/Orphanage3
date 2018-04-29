using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using OrphanageV3.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class FatherDbService : IFatherDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;
        private readonly IMotherDbService _motherDbService;
        private readonly ILogger _logger;

        public FatherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService,
            IMotherDbService motherDbService, ILogger logger)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _motherDbService = motherDbService;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.Persons.Father> GetFather(int Fid)
        {
            _logger.Information($"trying to get father with Id ({Fid})");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var father = await dbContext.Fathers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .FirstOrDefaultAsync(f => f.Id == Fid);

                if (father == null)
                {
                    _logger.Warning($"Father with id{Fid} cannot be found null is returned");
                    return null;
                }
                _selfLoopBlocking.BlockFatherSelfLoop(ref father);
                setFatherEntities(ref father, dbContext);
                _uriGenerator.SetFatherUris(ref father);
                father.OrphansCount = await GetOrphansCount(Fid, dbContext);
                father.WifeName = await (GetWifeName(father));
                _logger.Information($"returned Father with id {Fid}");
                return father;
            }
        }

        public async Task<string> GetWifeName(OrphanageDataModel.Persons.Father father)
        {
            _logger.Information($"Trying to get wife name for the father {father.Name.FullName()}");
            string WifeName = "";
            foreach (var fam in father.Families)
            {
                if (fam.Mother == null || fam.Mother.Name == null)
                {
                    _logger.Information($"Trying to get mother object for the father {father.Name.FullName()}");
                    fam.Mother = await _motherDbService.GetMother(fam.MotherId);
                }
                if (father.Families.Count > 1)
                    WifeName += fam.Mother.Name.FullName() + ", ";
                else
                    WifeName += fam.Mother.Name.FullName() + ", ";
            }
            if (WifeName.EndsWith(", "))
                WifeName = WifeName.Substring(0, WifeName.Length - 2);
            _logger.Information($"wife name for the father {father.Name.FullName()}, is {WifeName}");
            return WifeName;
        }

        public async Task<byte[]> GetFatherDeathCertificate(int Fid)
        {
            _logger.Information($"Trying to get Death Certificate, for the father with id {Fid}");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.DeathCertificatePhotoData }).FirstOrDefaultAsync();
                if (img?.DeathCertificatePhotoData != null)
                {
                    _logger.Information($"the Death Certificate of the father with id {Fid} has {img.DeathCertificatePhotoData.Length} bytes");
                    return img?.DeathCertificatePhotoData;
                }
                else
                {
                    _logger.Information($"the Death Certificate of the father with id {Fid} has 0 bytes, null will be returned");
                    return null;
                }
            }
        }

        public async Task<byte[]> GetFatherPhoto(int Fid)
        {
            _logger.Information($"Trying to Photo , for the father with id {Fid}");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.PhotoData }).FirstOrDefaultAsync();
                if (img?.PhotoData != null)
                {
                    _logger.Information($"the photo of the father with id {Fid} has {img.PhotoData.Length} bytes");
                    return img?.PhotoData;
                }
                else
                {
                    _logger.Information($"the photo of the father with id {Fid} has 0 bytes, null will be returned");
                    return null;
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int pageSize, int pageNum)
        {
            _logger.Information($"Trying to get Fathers with pageSize {pageSize} and pageNumber {pageNum}");
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FathersCount < totalSkiped)
                {
                    _logger.Warning($"Total skipped Fathers({totalSkiped}) are more than the count of all Fathers ({FathersCount})");
                    totalSkiped = FathersCount - pageSize;
                }
                if (totalSkiped < 0)
                {
                    _logger.Warning($"Total skipped Fathers({totalSkiped}) are less than zero");
                    totalSkiped = 0;
                }
                var fathers = await _orphanageDBC.Fathers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .ToListAsync();

                foreach (var father in fathers)
                {
                    OrphanageDataModel.Persons.Father fatherToFill = father;
                    setFatherEntities(ref fatherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockFatherSelfLoop(ref fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherToFill);
                    father.OrphansCount = await GetOrphansCount(father.Id, _orphanageDBC);
                    father.WifeName = await (GetWifeName(father));
                    fathersList.Add(fatherToFill);
                }
            }
            _logger.Information($"{fathersList.Count} records of fathers will be returned");
            return fathersList;
        }

        public async Task<int> GetFathersCount()
        {
            _logger.Information($"Trying to get all FathersCount");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int fathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                _logger.Information($"{fathersCount} is the count of all fathers");
                return fathersCount;
            }
        }

        public static void setFatherEntities(ref OrphanageDataModel.Persons.Father father, DbContext dbContext)
        {
            OrphanageDbCNoBinary dbCNoBinary = (OrphanageDbCNoBinary)dbContext;
            foreach (var fam in father.Families)
            {
                var moth = dbCNoBinary.Mothers.
                    Include(m => m.Name).
                    Include(m => m.Address).
                    FirstOrDefault(m => m.Id == fam.MotherId);
                fam.Mother = moth;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Fid)
        {
            _logger.Information($"Trying to get all orphan those related to the fathers with id ({Fid})");
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join fath in dbContext.Families.AsNoTracking() on orp.Family.FatherId equals fath.FatherId
                                     where orp.Family.FatherId == Fid
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
            _logger.Information($"father with id({Fid}) has {returnedOrphans.Count} related orphans.");
            return returnedOrphans;
        }

        ///<inheritdoc/>
        public async Task<int> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"Trying to add new father");
            if (father == null)
            {
                _logger.Error($"the parameter object father is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (father.Name == null)
            {
                _logger.Error($"the Name object of the parameter object father is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            if (!Properties.Settings.Default.ForceAdd)
            {
                _logger.Information($"ForceAdd option is not activated");
                if (Properties.Settings.Default.CheckName)
                {
                    _logger.Information($"CheckName option is activated, trying to get the equal names from database");
                    var ret = GetFathersByName(father.Name, orphanageDBC).FirstOrDefault();
                    if (ret != null)
                    {
                        _logger.Error($"father with id({father.Id}) has the same name, DuplicatedObjectException will be thrown");
                        throw new DuplicatedObjectException(father.GetType(), ret.GetType(), ret.Id);
                    }
                    else
                    {
                        _logger.Information($"didn't found any similar names to ({father.Name.FullName()}) in the database");
                    }
                }
            }
            var fatherName = father.Name;
            var taskFatherName = _regularDataService.AddName(fatherName, orphanageDBC);
            father.NameId = await taskFatherName;
            if (father.NameId == -1)
            {
                _logger.Warning($"Name object has not been added, nothing will be added, -1 will be returned");
                return -1;
            }
            father.Name = null;

            if (father.ActingUser != null)
            {
                father.ActingUser = null;
            }
            if (father.Families != null)
            {
                father.Families = null;
            }

            orphanageDBC.Fathers.Add(father);

            if (await orphanageDBC.SaveChangesAsync() == 1)
            {
                _logger.Information($"new father object with id {father.Id} has been added");
                return father.Id;
            }
            else
            {
                _logger.Warning($"something went wrong, nothing was added, -1 will be returned");
                return -1;
            }
        }

        public async Task<int> SaveFather(OrphanageDataModel.Persons.Father father)
        {
            _logger.Information($"Trying to save father");
            if (father == null)
            {
                _logger.Error($"the parameter object guarantor is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (father.NameId <= 0)
            {
                _logger.Error($"the NameID of the parameter object father equals {father.NameId}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;

                var fatherToReplace = await orphanageDc.Fathers.Where(f => f.Id == father.Id).FirstAsync();

                if (fatherToReplace == null)
                {
                    _logger.Error($"the original father object with id {father.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }
                _logger.Information($"processing the name object of the father with id({father.Id})");
                ret += await _regularDataService.SaveName(father.Name, orphanageDc);

                fatherToReplace.Birthday = father.Birthday;
                fatherToReplace.ColorMark = father.ColorMark;
                fatherToReplace.DateOfDeath = father.DateOfDeath;
                fatherToReplace.DeathReason = father.DeathReason;
                fatherToReplace.IdentityCardNumber = father.IdentityCardNumber;
                fatherToReplace.Jop = father.Jop;
                fatherToReplace.NameId = father.NameId;
                fatherToReplace.Note = father.Note;
                fatherToReplace.Story = father.Story;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"father with id({father.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return ret;
                }
                else
                {
                    _logger.Information($"nothing has changed, 0 will be returned");
                    return 0;
                }
            }
        }

        public async Task<bool> DeleteFather(int Fid, OrphanageDbCNoBinary orphanageDb)
        {
            _logger.Information($"trying to delete Father with id({Fid})");
            if (Fid <= 0)
            {
                _logger.Error($"the integer parameter Father ID is less than zero, false will be returned");
                return false;
            }
            var father = await orphanageDb.Fathers.Where(f => f.Id == Fid)
                .Include(f => f.Name)
                .Include(f => f.Families)
                .FirstOrDefaultAsync();

            if (father == null)
            {
                _logger.Error($"the original father object with id {Fid} object is not founded, ObjectNotFoundException will be thrown");
                throw new Exceptions.ObjectNotFoundException();
            }
            if (father.Families.Count > 0)
            {
                //the father has another family
                _logger.Error($"the father with id {Fid} has another family and can't be deleted, true will be returned");
                return true;
            }
            var fatherName = father.Name;
            orphanageDb.Fathers.Remove(father);
            orphanageDb.Names.Remove(fatherName);
            var ret = await orphanageDb.SaveChangesAsync();
            if (ret > 0)
            {
                _logger.Information($"father with id({Fid}) has been successfully deleted from the database");
                return true;
            }
            else
            {
                _logger.Information($"nothing has changed, false will be returned");
                return false;
            }
        }

        public async Task<bool> IsExist(int Fid)
        {
            if (Fid >= 0) throw new NullReferenceException();
            using (var orphangeDC = new OrphanageDbCNoBinary())
            {
                return await orphangeDC.Fathers.Where(f => f.Id == Fid).AnyAsync();
            }
        }

        public async Task SetFatherDeathCertificate(int Fid, byte[] data)
        {
            _logger.Information($"Trying to set Death Certificate, for the father with id {Fid}");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var father = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).FirstOrDefaultAsync();

                if (father == null)
                {
                    _logger.Error($"the original father object with id {Fid} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                father.DeathCertificatePhotoData = data;

                await _orphanageDBC.SaveChangesAsync();
                _logger.Information($"new value to Death Certificate has been successfully changed");
            }
        }

        public async Task SetFatherPhoto(int Fid, byte[] data)
        {
            _logger.Information($"Trying to set Photo, for the father with id {Fid}");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var father = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).FirstOrDefaultAsync();

                if (father == null)
                {
                    _logger.Error($"the original father object with id {Fid} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                father.PhotoData = data;

                await _orphanageDBC.SaveChangesAsync();
                _logger.Information($"new value to Photo has been successfully changed");
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Father> GetFathersByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get fathers with the similar name");
            if (nameObject == null)
            {
                _logger.Error($"name object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var fathers = orphanageDbCNo.Fathers
                        .Include(m => m.Name)
                        .ToArray();

            var Foundedfathers = fathers.Where(n => n.Name.Equals(nameObject));

            foreach (var father in Foundedfathers)
            {
                _logger.Information($"father with id({father.Id}) has the same Name as ({nameObject.FullName()})");
                yield return father;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(IList<int> fathersIds)
        {
            _logger.Information($"trying to get fathers with the given Id list");
            if (fathersIds == null || fathersIds.Count() == 0)
            {
                _logger.Information($"the given Id list is null or empty, null will be returned");
                return null;
            }
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var fathers = await _orphanageDBC.Fathers.AsNoTracking()
                    .Where(m => fathersIds.Contains(m.Id))
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .ToListAsync();

                foreach (var father in fathers)
                {
                    OrphanageDataModel.Persons.Father fatherToFill = father;
                    setFatherEntities(ref fatherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockFatherSelfLoop(ref fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherToFill);
                    father.OrphansCount = await GetOrphansCount(father.Id, _orphanageDBC);
                    father.WifeName = await (GetWifeName(father));
                    fathersList.Add(fatherToFill);
                }
            }
            _logger.Information($"{fathersList.Count} records of fathers will be returned");
            return fathersList;
        }

        public async Task<int> GetOrphansCount(int FatherId)
        {
            _logger.Information($"Trying to get orphans those are related to the father id {FatherId}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join fam in dbContext.Families.AsNoTracking() on orp.Family.FatherId equals fam.FatherId
                                     where fam.FatherId == FatherId
                                     select orp)
                              .CountAsync();
                _logger.Information($"{orphans} is the count of the related orphans of father ({FatherId})");
                return orphans;
            }
        }

        public async Task<int> GetOrphansCount(int FatherId, OrphanageDbCNoBinary dbContext)
        {
            _logger.Information($"Trying to get orphans those are related to the father id {FatherId}");
            var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                 join fam in dbContext.Families.AsNoTracking() on orp.Family.FatherId equals fam.FatherId
                                 where fam.FatherId == FatherId
                                 select orp)
                          .CountAsync();
            _logger.Information($"{orphans} is the count of the related orphans of father ({FatherId})");
            return orphans;
        }

        public async Task SetFatherColor(int Fid, int? value)
        {
            _logger.Information($"trying to set the color value ({value ?? -1}) to the father with Id({Fid})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var father = await _orphanageDBC.Fathers.Where(m => m.Id == Fid).FirstOrDefaultAsync();

                if (father == null)
                {
                    _logger.Error($"father with id ({Fid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                father.ColorMark = value;

                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"color value ({value ?? -1}) has been set successfully to the father with id({Fid})");
                }
                else
                {
                    _logger.Warning($"color value ({value ?? -1}) has not been set to the father with id({Fid}), nothing has changed");
                }
            }
        }
    }
}