using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
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

        public FamilyDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<IEnumerable<FamilyDC>> GetFamilies(int pageSize, int pageNum)
        {
            IList<FamilyDC> familiesList = new List<FamilyDC>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FamiliesCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FamiliesCount < totalSkiped)
                {
                    totalSkiped = FamiliesCount - pageSize;
                }
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .ToListAsync();

                foreach (var family in families)
                {
                    OrphanageDataModel.RegularData.Family familyToFill = family;
                    _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                    FamilyDC familyDC = Mapper.Map<FamilyDC>(familyToFill);
                    _uriGenerator.SetFamilyUris(ref familyDC);
                    familiesList.Add(familyDC);
                }
            }
            return familiesList;
        }

        public async Task<int> GetFamiliesCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int familiesCount = await _orphanageDBC.Families.AsNoTracking().CountAsync();
                return familiesCount;
            }
        }

        public async Task<FamilyDC> GetFamily(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var family = await _orphanageDBC.Families.AsNoTracking()
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .FirstOrDefaultAsync(f => f.Id == FamId);

                OrphanageDataModel.RegularData.Family familyToFill = family;
                _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                FamilyDC familyDC = Mapper.Map<FamilyDC>(familyToFill);
                _uriGenerator.SetFamilyUris(ref familyDC);
                return familyDC;
            }
        }

        public async Task<byte[]> GetFamilyCardPage1(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage1 }).FirstOrDefaultAsync();
                return img?.FamilyCardImagePage1;
            }
        }

        public async Task<byte[]> GetFamilyCardPage2(int FamId)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Families.AsNoTracking().Where(f => f.Id == FamId)
                    .Select(f => new { f.FamilyCardImagePage2 }).FirstOrDefaultAsync();
                return img?.FamilyCardImagePage2;
            }
        }

        public async Task<IEnumerable<OrphanDC>> GetOrphans(int FamId)
        {
            IList<OrphanDC> returnedOrphans = new List<OrphanDC>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await(from orp in dbContext.Orphans.AsNoTracking()
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
