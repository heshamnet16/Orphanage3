using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface ICaregiverDbService
    {
        Task<CaregiverDC> GetCaregiver(int Cid);

        Task<IEnumerable<CaregiverDC>> GetCaregivers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int Cid);

        Task<int> GetCaregiversCount();

        Task<byte[]> GetIdentityCardFace(int Cid);

        Task<byte[]> GetIdentityCardBack(int Cid);
    }
}
