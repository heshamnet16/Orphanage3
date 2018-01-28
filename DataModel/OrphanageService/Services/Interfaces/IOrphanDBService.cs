using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IOrphanDbService
    {
        Task<OrphanDto> GetOrphan(int id);

        Task<IEnumerable<OrphanDto>> GetOrphans(int pageSize, int pageNum);

        Task<int> GetOrphansCount();

        Task<byte[]> GetOrphanFaceImage(int Oid);

        Task<byte[]> GetOrphanBirthCertificate(int Oid);

        Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid);

        Task<byte[]> GetOrphanFullPhoto(int Oid);

        Task<byte[]> GetOrphanCertificate(int Oid);

        Task<byte[]> GetOrphanCertificate2(int Oid);

        Task<byte[]> GetOrphanHealthReporte(int Oid);
    }
}
