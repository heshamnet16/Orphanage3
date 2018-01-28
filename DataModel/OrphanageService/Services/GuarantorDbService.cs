﻿using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class GuarantorDbService : IGuarantorDbService
    {

        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public GuarantorDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }


        public async Task<IEnumerable<BailDto>> GetBails(int Gid)
        {
            IList<BailDto> returnedBails = new List<BailDto>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bails = await (from bail in dbContext.Bails.AsNoTracking()
                                   where bail.GuarantorID == Gid
                                   select bail)
                                     .Include(b => b.Account)
                              .ToListAsync();
                foreach (var bail in bails)
                {
                    var bailToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailToFill);
                    var bailDto = Mapper.Map<OrphanageDataModel.FinancialData.Bail, BailDto>(bailToFill);
                    returnedBails.Add(bailDto);
                }
            }
            return returnedBails;
        }

        public async Task<GuarantorDto> GetGuarantor(int Gid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var guarantor = await dbContext.Guarantors.AsNoTracking()
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .FirstOrDefaultAsync(c => c.Id == Gid);

                _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantor);
                GuarantorDto guarantorDC = Mapper.Map<GuarantorDto>(guarantor);
                return guarantorDC;
            }
        }

        public async Task<IEnumerable<GuarantorDto>> GetGuarantors(int pageSize, int pageNum)
        {
            IList<GuarantorDto> guarantorsList = new List<GuarantorDto>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                if (guarantorsCount < totalSkiped)
                {
                    totalSkiped = guarantorsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var guarantors = await _orphanageDBC.Guarantors.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .ToListAsync();

                foreach (var guarantor in guarantors)
                {
                    OrphanageDataModel.Persons.Guarantor guarantorToFill = guarantor;
                    _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                    GuarantorDto guarantorDto = Mapper.Map<GuarantorDto>(guarantorToFill);
                    guarantorsList.Add(guarantorDto);
                }
            }
            return guarantorsList;
        }

        public async Task<int> GetGuarantorsCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                return guarantorsCount;
            }
        }

        public async Task<IEnumerable<OrphanDto>> GetOrphans(int Gid)
        {
            IList<OrphanDto> returnedOrphans = new List<OrphanDto>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join
bail in dbContext.Bails.AsNoTracking() on orp.BailId equals bail.Id
                                     join
guar in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals guar.Id
                                     where guar.Id == Gid
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
                var FamOrphans = await (from fam in dbContext.Families.AsNoTracking()
                                        join
bail in dbContext.Bails.AsNoTracking() on fam.BailId equals bail.Id
                                        join
gura in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals gura.Id
                                        where gura.Id == Gid
                                        select fam)
                                        .Include(f => f.Orphans)
                                        .ToListAsync();
                foreach (var fam in FamOrphans)
                    orphans.AddRange(fam.Orphans);
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