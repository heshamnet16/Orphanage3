using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrphanageService.Services.Interfaces
{
    public interface IMotherDbService
    {
        Task<OrphanageDataModel.Persons.Mother> GetMother(int Mid);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Mid);

        Task<int> GetMotherCount();

        Task<byte[]> GetMotherIdPhotoFace(int Mid);

        Task<byte[]> GetMotherIdPhotoBack(int Mid);
    }
}
