using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

namespace OrphanageService.Orphan
{
    public class DBService
    {
        public static async Task<OrphanDC> GetOrphan(int id)
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

        public static async Task<IEnumerable<OrphanDC>> GetOrphans(int pageSize, int pageNum)
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

        public static async Task<byte[]> getOrphanFaceImage(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FacePhotoData;
            }
        }

        public static async Task<byte[]> getOrphanBirthCertificate(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.BirthCertificatePhotoData;
            }
        }

        public static async Task<byte[]> getOrphanFamilyCardPagePhoto(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FamilyCardPagePhotoData;
            }
        }

        public static async Task<byte[]> getOrphanFullPhoto(int Oid)
        {
            using (var dbContext = new OrphanageDBC())
            {
                var img = await dbContext.Orphans.FirstOrDefaultAsync(o => o.Id == Oid);
                return img.FullPhotoData;
            }
        }
    }
}
