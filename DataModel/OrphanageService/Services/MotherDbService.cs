using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
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

        public async Task<MotherDC> GetMother(int Mid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var mother = await dbContext.Mothers.AsNoTracking()
                    .Include(m => m.Families)
                    .Include(m => m.Name)
                    .FirstOrDefaultAsync(m => m.Id == Mid);

                _selfLoopBlocking.BlockMotherSelfLoop(ref mother);
                setMotherEntities(ref mother, dbContext);
                MotherDC motherDC = Mapper.Map<MotherDC>(mother);
                _uriGenerator.SetMotherUris(ref motherDC);
                return motherDC;
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

        public async Task<IEnumerable<MotherDC>> GetMothers(int pageSize, int pageNum)
        {
            IList<MotherDC> mothersList = new List<MotherDC>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int MothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                if (MothersCount < totalSkiped)
                {
                    totalSkiped = MothersCount - pageSize;
                }
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
                    MotherDC motherDC = Mapper.Map<MotherDC>(motherToFill);
                    _uriGenerator.SetMotherUris(ref motherDC);
                    mothersList.Add(motherDC);
                }
            }
            return mothersList;
        }

        public async Task<IEnumerable<OrphanDC>> GetOrphans(int Mid)
        {
            IList<OrphanDC> returnedOrphans = new List<OrphanDC>();
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
                    var orphanDC = Mapper.Map<OrphanageDataModel.Persons.Orphan, OrphanDC>(orpToFill);
                    _uriGenerator.SetOrphanUris(ref orphanDC);
                    returnedOrphans.Add(orphanDC);
                }
            }
            return returnedOrphans;
        }
    }
}
