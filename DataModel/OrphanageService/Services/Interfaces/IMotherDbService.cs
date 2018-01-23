using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrphanageService.Services.Interfaces
{
    public interface IMotherDbService
    {
        Task<MotherDC> GetMother(int Mid);

        Task<IEnumerable<MotherDC>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int Mid);

        Task<int> GetMotherCount();

        Task<byte[]> GetMotherIdPhotoFace(int Mid);

        Task<byte[]> GetMotherIdPhotoBack(int Mid);
    }
}
