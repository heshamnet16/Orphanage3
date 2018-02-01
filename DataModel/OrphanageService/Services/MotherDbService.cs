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
    public class MotherDbService : IMotherDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public MotherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public static void setMotherEntities(ref OrphanageDataModel.Persons.Mother mother, DbContext dbContext)
        {
            OrphanageDbCNoBinary dbCNoBinary = (OrphanageDbCNoBinary)dbContext;
            foreach (var fam in mother.Families)
            {
                var father = dbCNoBinary.Fathers.
                    Include(m => m.Name).
                    FirstOrDefault(m => m.Id == fam.MotherId);
                fam.Father = father;
            }
        }

        public async Task<bool> AddMother(OrphanageDataModel.Persons.Mother mother, OrphanageDBC orphanageDBC, bool forceAdd)
        {
            if (mother == null) throw new NullReferenceException();
            //TODO #32 check the mother data (name)
            orphanageDBC.Mothers.Add(mother);

            if (await orphanageDBC.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public Task<bool> DeleteMother(int Mid)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OrphanageDataModel.Persons.Mother> GetMother(int Mid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var mother = await dbContext.Mothers.AsNoTracking()
                    .Include(m => m.Families)
                    .Include(m => m.Name)
                    .FirstOrDefaultAsync(m => m.Id == Mid);

                _selfLoopBlocking.BlockMotherSelfLoop(ref mother);
                setMotherEntities(ref mother, dbContext);
                _uriGenerator.SetMotherUris(ref mother);
                return mother;
            }
        }

        public async Task<int> GetMotherCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int mothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                return mothersCount;
            }
        }

        public async Task<byte[]> GetMotherIdPhotoBack(int Mid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Mothers.AsNoTracking().Where(m => m.Id == Mid).Select(m => new { m.IdentityCardPhotoBackData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoBackData;
            }
        }

        public async Task<byte[]> GetMotherIdPhotoFace(int Mid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Mothers.AsNoTracking().Where(m => m.Id == Mid).Select(m => new { m.IdentityCardPhotoFaceData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoFaceData;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Mother> mothersList = new List<OrphanageDataModel.Persons.Mother>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int MothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                if (MothersCount < totalSkiped)
                {
                    totalSkiped = MothersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var mothers = await _orphanageDBC.Mothers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .ToListAsync();

                foreach (var mother in mothers)
                {
                    OrphanageDataModel.Persons.Mother motherToFill = mother;
                    setMotherEntities(ref motherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockMotherSelfLoop(ref motherToFill);
                    _uriGenerator.SetMotherUris(ref motherToFill);
                    mothersList.Add(motherToFill);
                }
            }
            return mothersList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Mid)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join fam in dbContext.Families.AsNoTracking() on orp.Family.MotherId equals fam.MotherId
                                     where orp.Family.MotherId == Mid
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

        public async Task<bool> IsExist(OrphanageDataModel.Persons.Mother mother)
        {
            if (mother == null) throw new NullReferenceException();
            using (var orphangeDC = new OrphanageDbCNoBinary())
            {
                return await orphangeDC.Mothers.Where(m => m.Id == mother.Id).AnyAsync();
            }
        }

        public async Task<bool> SaveMother(OrphanageDataModel.Persons.Mother mother)
        {
            if (mother == null) throw new NullReferenceException();
            using (OrphanageDBC orphanageDc = new OrphanageDBC())
            {
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;
                var motherToReplace = await orphanageDc.Mothers.Where(m => m.Id == mother.Id).FirstAsync();
                if (motherToReplace == null) throw new ObjectNotFoundException();
                motherToReplace = mother;
                await orphanageDc.SaveChangesAsync();
            }
            return true;
        }
    }
}
