using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Mother.Controllers
{
    [RoutePrefix("api/mother")]
    public class MothersController : ApiController
    {
        private readonly IMotherDbService _MotherDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;

        public MothersController(IMotherDbService motherDBService, IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _MotherDBService = motherDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
        }

        //api/mother/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Mother> Get(int id)
        {
            var ret = await _MotherDBService.GetMother(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(OrphanageDataModel.Persons.Mother mother)
        {
            var ret = 0;

            ret = await _MotherDBService.SaveMother(mother);

            if (ret > 0)
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
        public async Task<HttpResponseMessage> SetMotherColor(int motherId, int? colorValue)
        {
            if (colorValue == -1)
                await _MotherDBService.SetMotherColor(motherId, null);
            else
                await _MotherDBService.SetMotherColor(motherId, colorValue);

            return _httpMessageConfiguerer.OK();
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> Get(int pageSize, int pageNumber)
        {
            return await _MotherDBService.GetMothers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetMotherCount()
        {
            return await _MotherDBService.GetMotherCount();
        }

        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetByIds([FromUri] IList<int> motherIds)
        {
            var ret = await _MotherDBService.GetMothers(motherIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("orphans/{MotherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int MotherID)
        {
            return await _MotherDBService.GetOrphans(MotherID);
        }

        [HttpGet]
        [Route("orphans/count/{MotherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount(int MotherID)
        {
            return await _MotherDBService.GetOrphansCount(MotherID);
        }
    }
}