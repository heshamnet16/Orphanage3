using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using OrphanageService.Utilities.Interfaces;
using OrphanageService.DataContext;
using AutoMapper;
using System.Linq;

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

        public async Task<BailDto> GetBailDC(int Bid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bail = await dbContext.Bails.AsNoTracking()
                    .Include(b=>b.Account)
                    .Include(b => b.Guarantor)
                    .FirstOrDefaultAsync(b => b.Id == Bid);

                _selfLoopBlocking.BlockBailSelfLoop(ref bail);
                BailDto bailDto = Mapper.Map<BailDto>(bail);
                return bailDto;
            }
        }

        public async Task<IEnumerable<BailDto>> GetBails(int pageSize, int pageNum)
        {
            IList<BailDto> bailsList = new List<BailDto>();
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
                    BailDto bailDto = Mapper.Map<BailDto>(bailsToFill);
                    bailsList.Add(bailDto);
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

        public async Task<IEnumerable<FamilyDto>> GetFamilies(int Bid)
        {
            IList<FamilyDto> returnedFamilies = new List<FamilyDto>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var families = await(from orp in dbContext.Families.AsNoTracking()
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
                    var famDto = Mapper.Map<OrphanageDataModel.RegularData.Family, FamilyDto>(famToFill);
                    _uriGenerator.SetFamilyUris(ref famDto);
                    returnedFamilies.Add(famDto);
                }
            }
            return returnedFamilies;
        }

        public async Task<IEnumerable<OrphanDto>> GetOrphans(int Bid)
        {
            IList<OrphanDto> returnedOrphans = new List<OrphanDto>();
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
                    var orphanDto = Mapper.Map<OrphanageDataModel.Persons.Orphan, OrphanDto>(orpToFill);
                    _uriGenerator.SetOrphanUris(ref orphanDto);
                    returnedOrphans.Add(orphanDto);
                }
            }
            return returnedOrphans;
        }
    }
}
