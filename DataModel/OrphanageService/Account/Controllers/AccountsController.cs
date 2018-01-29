using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Account.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountsController : ApiController 
    {
        private IAccountDbService _accountDbService;

        public AccountsController(IAccountDbService accountDbService)
        {
            _accountDbService = accountDbService;
        }

        //api/account/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<AccountDto> Get(int id)
        {
            return await _accountDbService.GetAccountDto(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<AccountDto>> Get(int pageSize, int pageNumber)
        {
            return await _accountDbService.GetAccounts(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetAccountsCount()
        {
            return await _accountDbService.GetAccountsCount();
        }

        [HttpGet]
        [Route("bails/{AId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<BailDto>> GetBails(int AId)
        {
            return await _accountDbService.GetBails(AId);
        }

        [HttpGet]
        [Route("guarantors/{AId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<GuarantorDto>> GetGuarantors(int AId)
        {
            return await _accountDbService.GetGuarantors(AId);
        }
    }
}
