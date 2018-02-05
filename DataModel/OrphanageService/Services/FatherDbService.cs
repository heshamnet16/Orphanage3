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
    public class FatherDbService : IFatherDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;

        public FatherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
        }

        public async Task<OrphanageDataModel.Persons.Father> GetFather(int Fid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var father = await dbContext.Fathers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .FirstOrDefaultAsync(f => f.Id == Fid);

                if (father == null) return null;
                _selfLoopBlocking.BlockFatherSelfLoop(ref father);
                setFatherEntities(ref father, dbContext);
                _uriGenerator.SetFatherUris(ref father);
                return father;
            }
        }

        public async Task<byte[]> GetFatherDeathCertificate(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.DeathCertificatePhotoData }).FirstOrDefaultAsync();
                return img?.DeathCertificatePhotoData;
            }
        }

        public async Task<byte[]> GetFatherPhoto(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.PhotoData }).FirstOrDefaultAsync();
                return img?.PhotoData;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FathersCount < totalSkiped)
                {
                    totalSkiped = FathersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
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
                    fathersList.Add(fatherToFill);
                }
            }
            return fathersList;
        }

        public async Task<int> GetFathersCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                return orphansCount;
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
            return returnedOrphans;
        }

        ///<inheritdoc/>
        public async Task<int> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDbCNoBinary orphanageDBC)
        {
            if (father == null) throw new NullReferenceException();
            //TODO #32 check the father data (name)
            //TODO use forceadd in the settings
            orphanageDBC.Fathers.Add(father);

            if (await orphanageDBC.SaveChangesAsync() == 1)
                return father.Id;
            else
                return -1;
        }

        public async Task<int> SaveFather(OrphanageDataModel.Persons.Father father)
        {
            if (father == null) throw new NullReferenceException();
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                var fatherToReplace = await orphanageDc.Fathers.Where(f => f.Id == father.Id).FirstAsync();
                if (fatherToReplace == null) throw new ObjectNotFoundException();
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
                return ret;
            }
        }

        public async Task<bool> DeleteFather(int Fid, OrphanageDbCNoBinary orphanageDb)
        {
            if (Fid == 0) throw new NullReferenceException();
            var father = await orphanageDb.Fathers.Where(f => f.Id == Fid)
                .Include(f => f.Name)
                .Include(f=>f.Families)
                .FirstOrDefaultAsync();
            if (father.Families.Count > 0)
            {
                //the father has another family
                return true;
            }
            var fatherName = father.Name;
            orphanageDb.Fathers.Remove(father);
            orphanageDb.Names.Remove(fatherName);
            if (await orphanageDb.SaveChangesAsync() > 1)
                return true;
            else
                return false;
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
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var father = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).FirstOrDefaultAsync();

                if (father == null)
                    return;
                father.DeathCertificatePhotoData = data;

                await _orphanageDBC.SaveChangesAsync();
            }
        }

        public async Task SetFatherPhoto(int Fid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var father = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).FirstOrDefaultAsync();

                if (father == null)
                    return;

                father.PhotoData = data;

                await _orphanageDBC.SaveChangesAsync();
            }
        }
    }
}