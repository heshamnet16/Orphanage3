using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Account.Controllers
{
    [Authorize(Roles = "Admin, CanRead")]
    [RoutePrefix("api/account")]
    public class AccountsController : ApiController
    {
        private IAccountDbService _accountDbService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;

        public AccountsController(IAccountDbService accountDbService,
            IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _accountDbService = accountDbService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
        }

        //api/account/{id}
        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.FinancialData.Account> Get(int id)
        {
            var ret = await _accountDbService.GetAccount(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [Authorize(Roles = "Admin, CanDelete")]
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object account)
        {
            var accountEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Account>(account.ToString());
            var ret = false;

            ret = await _accountDbService.SaveAccount(accountEntity);

            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [Authorize(Roles = "Admin, CanAdd")]
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(object account)
        {
            var accountEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Account>(account.ToString());
            OrphanageDataModel.FinancialData.Account ret = null;

            ret = await _accountDbService.AddAccount(accountEntity);

            if (ret != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, ret);
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [Authorize(Roles = "Admin, CanDelete")]
        [HttpDelete]
        [Route("{AID}")]
        public async Task<HttpResponseMessage> Delete(int AID)
        {
            var ret = await _accountDbService.DeleteAccount(AID);
            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> Get(int pageSize, int pageNumber)
        {
            return await _accountDbService.GetAccounts(pageSize, pageNumber);
        }

        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetByIds([FromUri] IList<int> accoutnsIds)
        {
            if (accoutnsIds == null || accoutnsIds.Count == 0) return null;
            var ret = await _accountDbService.GetAccounts(accoutnsIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetAccountsCount()
        {
            return await _accountDbService.GetAccountsCount();
        }

        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("bails/{AId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int AId)
        {
            return await _accountDbService.GetBails(AId);
        }

        [Authorize(Roles = "Admin, CanRead")]
        [HttpGet]
        [Route("guarantors/{AId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int AId)
        {
            return await _accountDbService.GetGuarantors(AId);
        }
    }
}