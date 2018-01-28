using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrphanageService.Services.Interfaces
{
    public interface IMotherDbService
    {
        Task<MotherDto> GetMother(int Mid);

        Task<IEnumerable<MotherDto>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Mid);

        Task<int> GetMotherCount();

        Task<byte[]> GetMotherIdPhotoFace(int Mid);

        Task<byte[]> GetMotherIdPhotoBack(int Mid);
    }
}
