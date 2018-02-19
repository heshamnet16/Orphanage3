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
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetCaregiversCount()
        {
            return await _GuarantorDBService.GetGuarantorsCount();
        }

        [HttpGet]
        [Route("orphans/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetFamilyOrphans(int GId)
        {
            return await _GuarantorDBService.GetOrphans(GId);
        }

        [HttpGet]
        [Route("bails/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int GId)
        {
            return await _GuarantorDBService.GetBails(GId);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object guarantor)
        {
            var guarantorEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Guarantor>(guarantor.ToString());
            var ret = await _GuarantorDBService.SaveGuarantor(guarantorEntity);
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
        public async Task<HttpResponseMessage> Post(object guarantor)
        {
            var guarantorEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Guarantor>(guarantor.ToString());
            var ret = await _GuarantorDBService.AddGuarantor(guarantorEntity);
            if (ret > 0)
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
        public async Task<HttpResponseMessage> Delete(int GID)
        {
            var ret = await _GuarantorDBService.DeleteGuarantor(GID);
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