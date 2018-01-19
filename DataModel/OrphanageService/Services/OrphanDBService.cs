using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class OrphanDBService : IOrphanDBService
    {
        private readonly OrphanageDBC _orphanageDBC;
        #region Private Functions
        private void setOrphanUrl(ref OrphanDC orphanDC)
        {
            orphanDC.FacePhotoURI = "api/orphan/media/face/" + orphanDC.Id;
            orphanDC.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphanDC.Id;
            orphanDC.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphanDC.Id;
            orphanDC.FullPhotoURI = "api/orphan/media/full/" + orphanDC.Id;
        }
        #endregion

        public OrphanDBService(OrphanageDBC orphanageDBC)
        {
            _orphanageDBC = orphanageDBC;
        }
        public async Task<OrphanDC> GetOrphan(int id)
        {
            using (_orphanageDBC)
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
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var orphans = await _orphanageDBC.Orphans.OrderBy(o=>o.Id).Skip(pageSize * pageNum).Take(pageSize)
                    .Include(o=> o.Education)
                    .Include(o => o.Name)
                    .Include(o => o.Bail)
                    .Include(o => o.Caregiver)
                    .Include(o => o.Family)
                    .Include(o => o.Guarantor)
                    .Include(o => o.HealthStatus)
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
            using (_orphanageDBC)
            {
                var img = await _orphanageDBC.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FacePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            using (_orphanageDBC)
            {
                var img = await _orphanageDBC.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.BirthCertificatePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            using (_orphanageDBC)
            {
                var img = await _orphanageDBC.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FamilyCardPagePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            using (_orphanageDBC)
            {
                var img = await _orphanageDBC.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FullPhotoData;
            }
        }
    }
}
