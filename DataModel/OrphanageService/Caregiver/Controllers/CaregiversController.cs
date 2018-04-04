using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
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

        public CaregiversController(ICaregiverDbService caregiverDBService, IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _CaregiverDBService = caregiverDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
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

            ret = await _CaregiverDBService.SaveCaregiver(caregiverEntity);

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
        public async Task<HttpResponseMessage> SetCaregiverColor(int CaregiverId, int colorValue)
        {
            if (colorValue == -1)
                await _CaregiverDBService.SetCaregiverColor(CaregiverId, null);
            else
                await _CaregiverDBService.SetCaregiverColor(CaregiverId, colorValue);

            return _httpMessageConfiguerer.OK();
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(object caregiver)
        {
            var caregiverEntity = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Caregiver>(caregiver.ToString());
            OrphanageDataModel.Persons.Caregiver ret = null;

            ret = await _CaregiverDBService.AddCaregiver(caregiverEntity);

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