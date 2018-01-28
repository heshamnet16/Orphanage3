using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFamilyDbService
    {
        Task<FamilyDto> GetFamily(int FamId);

        Task<IEnumerable<FamilyDto>> GetFamilies(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDto>> GetOrphans(int FamId);

        Task<int> GetFamiliesCount();

        Task<byte[]> GetFamilyCardPage1(int FamId);

        Task<byte[]> GetFamilyCardPage2(int FamId);

    }
}
