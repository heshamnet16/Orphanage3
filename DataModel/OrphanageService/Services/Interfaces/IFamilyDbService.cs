using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFamilyDbService
    {
        Task<OrphanageDataModel.RegularData.Family> GetFamily(int FamId);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FamId);

        Task<int> GetFamiliesCount();

        Task<byte[]> GetFamilyCardPage1(int FamId);

        Task<byte[]> GetFamilyCardPage2(int FamId);

    }
}
