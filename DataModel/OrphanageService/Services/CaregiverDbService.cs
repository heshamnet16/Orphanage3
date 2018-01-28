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
    public class CaregiverDbService : ICaregiverDbService
    {

        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public CaregiverDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<CaregiverDC> GetCaregiver(int Cid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var caregiver = await dbContext.Caregivers.AsNoTracking()
                    .Include(c=>c.Address)
                    .Include(c => c.Name)
                    .Include(c=>c.Orphans)
                    .FirstOrDefaultAsync(c => c.Id == Cid);

                _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiver);
                CaregiverDC caregiverDC = Mapper.Map<CaregiverDC>(caregiver);
                _uriGenerator.SetCaregiverUris(ref caregiverDC);
                return caregiverDC;
            }
        }

        public async Task<IEnumerable<CaregiverDC>> GetCaregivers(int pageSize, int pageNum)
        {
            IList<CaregiverDC> caregiversList = new List<CaregiverDC>();
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
                    .ToListAsync();

                foreach (var caregiver in caregivers)
                {
                    OrphanageDataModel.Persons.Caregiver caregiverToFill = caregiver;
                    _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiverToFill);
                    CaregiverDC caregiverDC = Mapper.Map<CaregiverDC>(caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverDC);
                    caregiversList.Add(caregiverDC);
                }
            }
            return caregiversList;
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

        public async Task<IEnumerable<OrphanDC>> GetOrphans(int Cid)
        {
            IList<OrphanDC> returnedOrphans = new List<OrphanDC>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await(from orp in dbContext.Orphans.AsNoTracking()                                   
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
                    var orphanDC = Mapper.Map<OrphanageDataModel.Persons.Orphan, OrphanDC>(orpToFill);
                    _uriGenerator.SetOrphanUris(ref orphanDC);
                    returnedOrphans.Add(orphanDC);
                }
            }
            return returnedOrphans;
        }
    }
}
