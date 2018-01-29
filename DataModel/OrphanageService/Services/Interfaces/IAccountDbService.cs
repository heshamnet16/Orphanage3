using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IAccountDbService
    {
        Task<AccountDto> GetAccountDto(int Aid);

        Task<IEnumerable<AccountDto>> GetAccounts(int pageSize, int pageNum);

        Task<IEnumerable<BailDto>> GetBails(int Aid);

        Task<IEnumerable<GuarantorDto>> GetGuarantors(int Aid);

        Task<int> GetAccountsCount();
    }
}
