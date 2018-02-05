using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class BailDbService : IBailDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public BailDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<OrphanageDataModel.FinancialData.Bail> GetBailDC(int Bid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bail = await dbContext.Bails.AsNoTracking()
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .FirstOrDefaultAsync(b => b.Id == Bid);

                if (bail == null) return null;
                _selfLoopBlocking.BlockBailSelfLoop(ref bail);
                return bail;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.FinancialData.Bail> bailsList = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int bailsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                if (bailsCount < totalSkiped)
                {
                    totalSkiped = bailsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .ToListAsync();

                foreach (var bail in bails)
                {
                    OrphanageDataModel.FinancialData.Bail bailsToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailsToFill);
                    bailsList.Add(bailsToFill);
                }
            }
            return bailsList;
        }

        public async Task<int> GetBailsCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int bailsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                return bailsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Bid)
        {
            IList<OrphanageDataModel.RegularData.Family> returnedFamilies = new List<OrphanageDataModel.RegularData.Family>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var families = await (from orp in dbContext.Families.AsNoTracking()
                                      where orp.BailId == Bid
                                      select orp)
                                    .Include(f => f.AlternativeAddress)
                                    .Include(f => f.Bail)
                                    .Include(f => f.Father)
                                    .Include(f => f.Mother)
                                    .Include(f => f.Orphans)
                                    .Include(f => f.PrimaryAddress)
                              .ToListAsync();

                foreach (var fam in families)
                {
                    var famToFill = fam;
                    _selfLoopBlocking.BlockFamilySelfLoop(ref famToFill);
                    _uriGenerator.SetFamilyUris(ref famToFill);
                    returnedFamilies.Add(famToFill);
                }
            }
            return returnedFamilies;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Bid)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     where orp.BailId == Bid
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
    }
}