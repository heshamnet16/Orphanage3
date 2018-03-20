using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Caregiver.Controllers
{
    [RoutePrefix("api/caregiver")]
    public class CaregiversController : ApiController
    {
        private readonly ICaregiverDbService _CaregiverDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;
        private readonly IExceptionHandlerService _exceptionHandlerService;

        public CaregiversController(ICaregiverDbService caregiverDBService, IHttpMessageConfiguerer httpMessageConfiguerer, IExceptionHandlerService exceptionHandlerService)
        {
            _CaregiverDBService = caregiverDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
            _exceptionHandlerService = exceptionHandlerService;
        }

        //api/caregiver/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Caregiver> Get(int id)
        {
            var ret = await _CaregiverDBService.GetCaregiver(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> Get(int pageSize, int pageNumber)
        {
            return await _CaregiverDBService.GetCaregivers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetCaregiversCount()
        {
            return await _CaregiverDBService.GetCaregiversCount();
        }

        [HttpGet]
        [Route("orphans/{CId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetFamilyOrphans(int CId)
        {
            return await _CaregiverDBService.GetOrphans(CId);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object caregiver)
        {
            var caregiverEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Caregiver>(caregiver.ToString());
            var ret = false;
            try
            {
                ret = await _CaregiverDBService.SaveCaregiver(caregiverEntity);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
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
        public async Task<HttpResponseMessage> Post(object caregiver)
        {
            var caregiverEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Caregiver>(caregiver.ToString());
            var ret = 0;
            try
            {
                ret = await _CaregiverDBService.AddCaregiver(caregiverEntity);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
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
        [Route("{CID}")]
        public async Task<HttpResponseMessage> Delete(int CID)
        {
            var ret = await _CaregiverDBService.DeleteCaregiver(CID);
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