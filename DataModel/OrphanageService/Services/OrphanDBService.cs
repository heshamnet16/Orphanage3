using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class OrphanDbService : IOrphanDbService
    {
        private readonly ISelfLoopBlocking _loopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public OrphanDbService(ISelfLoopBlocking loopBlocking, IUriGenerator uriGenerator)
        {
            _loopBlocking = loopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> GetOrphan(int id)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()
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
                    .FirstOrDefaultAsync(o => o.Id == id);

                _loopBlocking.BlockOrphanSelfLoop(ref orphan);
                _uriGenerator.SetOrphanUris(ref orphan);
                return orphan;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                if (orphansCount < totalSkiped)
                {
                    totalSkiped = orphansCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(o => o.Name)
                    .Include(o => o.Caregiver.Name)
                    .Include(o => o.Caregiver.Address)
                    .Include(o => o.Family.Father.Name)
                    .Include(o => o.Family.Mother.Name)
                    .Include(o => o.Family.PrimaryAddress)
                    .Include(o => o.Family.AlternativeAddress)
                    .Include(o => o.Guarantor.Name)
                    .ToListAsync();

                foreach (var orphan in orphans)
                {
                    var orphanTofill = orphan;
                    _loopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanTofill);
                    orphansList.Add(orphanTofill);
                }
            }
            return orphansList;
        }

        public async Task<int> GetOrphansCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                return orphansCount;
            }
        }

        public async Task<byte[]> GetOrphanFaceImage(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FacePhotoData }).FirstOrDefaultAsync();
                return img?.FacePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.BirthCertificatePhotoData }).FirstOrDefaultAsync();
                return img?.BirthCertificatePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FamilyCardPagePhotoData }).FirstOrDefaultAsync();
                return img?.FamilyCardPagePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FullPhotoData }).FirstOrDefaultAsync();
                return img?.FullPhotoData;
            }
        }

        public async Task<byte[]> GetOrphanCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoFront }).FirstOrDefaultAsync();
                return img?.CertificatePhotoFront;
            }
        }

        public async Task<byte[]> GetOrphanCertificate2(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoBack }).FirstOrDefaultAsync();
                return img?.CertificatePhotoBack;
            }
        }

        public async Task<byte[]> GetOrphanHealthReporte(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.HealthId.HasValue).Select(o => new { o.HealthStatus.ReporteFileData }).FirstOrDefaultAsync();
                return img?.ReporteFileData;
            }
        }
    }
}