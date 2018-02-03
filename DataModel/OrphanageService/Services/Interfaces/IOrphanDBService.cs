using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IOrphanDbService
    {
        Task<OrphanageDataModel.Persons.Orphan> GetOrphan(int id);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetBrothers(int Oid);

        Task<int> GetBrothersCount(int Oid);

        Task<int> GetOrphansCount();

        Task<byte[]> GetOrphanFaceImage(int Oid);

        Task<byte[]> GetOrphanBirthCertificate(int Oid);

        Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid);

        Task<byte[]> GetOrphanFullPhoto(int Oid);

        Task<byte[]> GetOrphanCertificate(int Oid);

        Task<byte[]> GetOrphanCertificate2(int Oid);

        Task<byte[]> GetOrphanHealthReporte(int Oid);

        Task<bool> SetOrphanFaceImage(int Oid, byte[] data);

        Task<bool> SetOrphanBirthCertificate(int Oid, byte[] data);

        Task<bool> SetOrphanFamilyCardPagePhoto(int Oid, byte[] data);

        Task<bool> SetOrphanFullPhoto(int Oid, byte[] data);

        Task<bool> SetOrphanCertificate(int Oid, byte[] data);

        Task<bool> SetOrphanCertificate2(int Oid, byte[] data);

        Task<bool> SetOrphanHealthReporte(int Oid, byte[] data);
    }
}