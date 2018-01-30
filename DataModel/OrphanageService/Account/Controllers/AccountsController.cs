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
        public async Task<OrphanageDataModel.FinancialData.Account> Get(int id)
        {
            return await _accountDbService.GetAccount(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> Get(int pageSize, int pageNumber)
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
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int AId)
        {
            return await _accountDbService.GetBails(AId);
        }

        [HttpGet]
        [Route("guarantors/{AId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int AId)
        {
            return await _accountDbService.GetGuarantors(AId);
        }
    }
}
