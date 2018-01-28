using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IGuarantorDbService
    {
        Task<GuarantorDC> GetGuarantor(int Gid);

        Task<IEnumerable<GuarantorDC>> GetGuarantors(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int Gid);

        Task<int> GetGuarantorsCount();

        Task<IEnumerable<BailDC>> GetBails(int Gid);

    }
}
