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
        public  async Task<OrphanDC> GetOrphan(int id)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var orphan  = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == id);
                orphan.FacePhotoURI = "api/orphan/media/face/" + id;
                orphan.BirthCertificatePhotoURI = "api/orphan/media/birth/" + id;
                orphan.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + id;
                orphan.FullPhotoURI = "api/orphan/media/full/" + id;
                return Mapper.Map<OrphanDC>(orphan);
            }
        }

        public  async Task<IEnumerable<OrphanDC>> GetOrphans(int pageSize, int pageNum)
        {
            IList<OrphanDC> orphansList = new List<OrphanDC>();
            using (var dbContext = new OrphanageDBC())
            {
                var orphans = await dbContext.Orphans.OrderBy(o=>o.Id).Skip(pageSize * pageNum).Take(pageSize).ToListAsync();
                foreach (var orphan in orphans)
                {
                    orphan.FacePhotoURI = "api/orphan/media/face/" + orphan.Id;
                    orphan.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphan.Id;
                    orphan.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphan.Id;
                    orphan.FullPhotoURI = "api/orphan/media/full/" + orphan.Id;
                    orphansList.Add(Mapper.Map<OrphanDC>(orphan));
                }
            }
            return orphansList;
        }

        public  async Task<byte[]> GetOrphanFaceImage(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FacePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.BirthCertificatePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FamilyCardPagePhotoData;
            }
        }

        public  async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FullPhotoData;
            }
        }
    }
}
