using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Bail.Controllers
{
    [RoutePrefix("api/bail")]
    public class BailsController : ApiController
    {
        private IBailDbService _bailDbService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;

        public BailsController(IBailDbService bailDbService, IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _bailDbService = bailDbService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
        }

        //api/bail/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.FinancialData.Bail> Get(int id)
        {
            var ret = await _bailDbService.GetBailDC(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> Get(int pageSize, int pageNumber)
        {
            return await _bailDbService.GetBails(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("IsFamily/{value}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBailsByFamily(bool value)
        {
            return await _bailDbService.GetBails(value);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetBailsCount()
        {
            return await _bailDbService.GetBailsCount();
        }

        [HttpGet]
        [Route("orphans/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int BId)
        {
            return await _bailDbService.GetOrphans(BId);
        }

        [HttpGet]
        [Route("families/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int BId)
        {
            return await _bailDbService.GetFamilies(BId);
        }

        [HttpGet]
        [Route("orphans/count/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount(int BId)
        {
            return await _bailDbService.GetOrphansCount(BId);
        }

        [HttpGet]
        [Route("families/count/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetFamiliesCount(int BId)
        {
            return await _bailDbService.GetFamiliesCount(BId);
        }

        [HttpGet]
        [Route("orphans/ids/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<int>> GetOrphansIds(int BId)
        {
            return await _bailDbService.GetOrphansIds(BId);
        }

        [HttpGet]
        [Route("families/ids/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<int>> GetFamiliesIds(int BId)
        {
            return await _bailDbService.GetFamiliesIds(BId);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object bail)
        {
            var accountEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Bail>(bail.ToString());
            var ret = false;

            ret = await _bailDbService.SaveBail(accountEntity);

            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(object bail)
        {
            var bailEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Bail>(bail.ToString());
            OrphanageDataModel.FinancialData.Bail ret = null;

            ret = await _bailDbService.AddBail(bailEntity);

            if (ret != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, ret);
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpDelete]
        [Route("{CID}/{forceDelete}")]
        public async Task<HttpResponseMessage> Delete(int CID, bool forceDelete)
        {
            var ret = false;

            ret = await _bailDbService.DeleteBail(CID, forceDelete);

            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }
    }
}