using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IBailDbService
    {
        Task<BailDto> GetBailDC(int Bid);

        Task<IEnumerable<BailDto>> GetBails(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Bid);

        Task<IEnumerable<FamilyDto>> GetFamilies(int Bid);

        Task<int> GetBailsCount();

    }
}
