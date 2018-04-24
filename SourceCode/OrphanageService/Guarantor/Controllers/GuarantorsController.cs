using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Guarantor.Controllers
{
    [Authorize]
    [RoutePrefix("api/guarantor")]
    public class GuarantorsController : ApiController
    {
        private readonly IGuarantorDbService _GuarantorDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;

        public GuarantorsController(IGuarantorDbService guarantorDBService, IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _GuarantorDBService = guarantorDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
        }

        //api/guarantor/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Guarantor> Get(int id)
        {
            var ret = await _GuarantorDBService.GetGuarantor(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> Get(int pageSize, int pageNumber)
        {
            return await _GuarantorDBService.GetGuarantors(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetByIds([FromUri] IList<int> guarantorsIds)
        {
            if (guarantorsIds == null || guarantorsIds.Count == 0) return null;
            var ret = await _GuarantorDBService.GetGuarantors(guarantorsIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetGuarantorsCount()
        {
            return await _GuarantorDBService.GetGuarantorsCount();
        }

        [HttpGet]
        [Route("orphans/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int GId)
        {
            return await _GuarantorDBService.GetOrphans(GId);
        }

        [HttpGet]
        [Route("orphans/count/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount(int GId)
        {
            return await _GuarantorDBService.GetOrphansCount(GId);
        }

        [HttpGet]
        [Route("orphans/Ids/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<int>> GetOrphansIds(int GId)
        {
            return await _GuarantorDBService.GetOrphansIds(GId);
        }

        [HttpGet]
        [Route("families/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int GId)
        {
            return await _GuarantorDBService.GetFamilies(GId);
        }

        [HttpGet]
        [Route("families/count/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetFamiliesCount(int GId)
        {
            return await _GuarantorDBService.GetFamiliesCount(GId);
        }

        [HttpGet]
        [Route("families/Ids/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<int>> GetFamiliesIds(int GId)
        {
            return await _GuarantorDBService.GetFamiliesIds(GId);
        }

        [HttpGet]
        [Route("bails/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int GId)
        {
            return await _GuarantorDBService.GetBails(GId);
        }

        [HttpGet]
        [Route("bails/count/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetBailsCount(int GId)
        {
            return await _GuarantorDBService.GetBailsCount(GId);
        }

        [HttpGet]
        [Route("bails/Ids/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<int>> GetBailsIds(int GId)
        {
            return await _GuarantorDBService.GetBailsIds(GId);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object guarantor)
        {
            var guarantorEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Guarantor>(guarantor.ToString());
            var ret = false;

            ret = await _GuarantorDBService.SaveGuarantor(guarantorEntity);

            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpPut]
        [Route("color")]
        public async Task<HttpResponseMessage> SetGuarantorColor(int guarantorId, int colorValue)
        {
            if (colorValue == -1)
                await _GuarantorDBService.SetGuarantorColor(guarantorId, null);
            else
                await _GuarantorDBService.SetGuarantorColor(guarantorId, colorValue);

            return _httpMessageConfiguerer.OK();
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(object guarantor)
        {
            var guarantorEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Guarantor>(guarantor.ToString());
            OrphanageDataModel.Persons.Guarantor ret = null;

            ret = await _GuarantorDBService.AddGuarantor(guarantorEntity);

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
        [Route("{GID}")]
        public async Task<HttpResponseMessage> Delete(int GID, bool forceDelete)
        {
            var ret = await _GuarantorDBService.DeleteGuarantor(GID, forceDelete);
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