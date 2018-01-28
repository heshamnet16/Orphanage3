using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IGuarantorDbService
    {
        Task<GuarantorDto> GetGuarantor(int Gid);

        Task<IEnumerable<GuarantorDto>> GetGuarantors(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Gid);

        Task<int> GetGuarantorsCount();

        Task<IEnumerable<BailDto>> GetBails(int Gid);

    }
}
