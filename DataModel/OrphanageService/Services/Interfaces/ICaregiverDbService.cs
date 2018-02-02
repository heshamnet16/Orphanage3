using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface ICaregiverDbService
    {
        Task<OrphanageDataModel.Persons.Caregiver> GetCaregiver(int Cid);

        Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Cid);

        Task<int> GetCaregiversCount();

        Task<byte[]> GetIdentityCardFace(int Cid);

        Task<byte[]> GetIdentityCardBack(int Cid);
    }
}