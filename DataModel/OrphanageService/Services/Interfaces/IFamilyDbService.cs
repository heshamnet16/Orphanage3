using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFamilyDbService
    {
        Task<FamilyDC> GetFamily(int FamId);

        Task<IEnumerable<FamilyDC>> GetFamilies(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int FamId);

        Task<int> GetFamiliesCount();

        Task<byte[]> GetFamilyCardPage1(int FamId);

        Task<byte[]> GetFamilyCardPage2(int FamId);

    }
}
