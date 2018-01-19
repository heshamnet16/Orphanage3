using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class OrphanDBService : IOrphanDBService
    {
        #region Private Functions
        private void setOrphanUrl(ref OrphanDC orphanDC)
        {
            orphanDC.FacePhotoURI = "api/orphan/media/face/" + orphanDC.Id;
            orphanDC.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphanDC.Id;
            orphanDC.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphanDC.Id;
            orphanDC.FullPhotoURI = "api/orphan/media/full/" + orphanDC.Id;
        }
        #endregion

        public OrphanDBService()
        {
            
        }
        public async Task<OrphanDC> GetOrphan(int id)
        {
            using (var _orphanageDBC = new OrphanageDBC(true))
            {
                var orphan  = await _orphanageDBC.Orphans
                    .Include(o=>o.Education)
                    .Include(o=>o.Name)
                    .Include(o=>o.Bail)
                    .Include(o=>o.Caregiver)
                    .Include(o=>o.Family)
                    .Include(o=>o.Guarantor)
                    .Include(o=>o.HealthStatus)
                    .FirstOrDefaultAsync(o => o.Id == id);
                OrphanDC orphanDC = Mapper.Map<OrphanDC>(orphan);
                setOrphanUrl(ref orphanDC);
                return orphanDC;
            }
        }       

        public  async Task<IEnumerable<OrphanDC>> GetOrphans(int pageSize, int pageNum)
        {
            IList<OrphanDC> orphansList = new List<OrphanDC>();
            using (var _orphanageDBC = new OrphanageDBC(true))
            {
                int totalSkiped = pageSize * pageNum;
                int orphansCount = await _orphanageDBC.Orphans.CountAsync();
                if(orphansCount<totalSkiped)
                {
                    totalSkiped = orphansCount - pageSize;
                }
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(()=> totalSkiped).Take(()=> pageSize)
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
                    OrphanDC orphanDC = Mapper.Map<OrphanDC>(orphan);
                    setOrphanUrl(ref orphanDC);
                    orphansList.Add(orphanDC);
                }
            }
            return orphansList;
        }

        public  async Task<byte[]> GetOrphanFaceImage(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid).Select(o=> new { o.FacePhotoData }).FirstOrDefaultAsync();
                return img.FacePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid).Select(o => new { o.BirthCertificatePhotoData }).FirstOrDefaultAsync();
                return img.BirthCertificatePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid).Select(o => new { o.FamilyCardPagePhotoData}).FirstOrDefaultAsync();
                return img.FamilyCardPagePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid).Select(o => new { o.FullPhotoData }).FirstOrDefaultAsync();
                return img.FullPhotoData;
            }
        }

        public async Task<byte[]> GetOrphanCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoFront }).FirstOrDefaultAsync();
                return img.CertificatePhotoFront;
            }
        }

        public async Task<byte[]> GetOrphanCertificate2(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoBack }).FirstOrDefaultAsync();
                return img.CertificatePhotoBack;
            }
        }

        public async Task<byte[]> GetOrphanHealthReporte(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Orphans.Where(o => o.Id == Oid && o.HealthId.HasValue).Select(o => new { o.HealthStatus.ReporteFileData }).FirstOrDefaultAsync();
                return img.ReporteFileData;
            }
        }
    }
}
