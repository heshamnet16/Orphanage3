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
            var ret = await _MotherDBService.SaveMother(mother);
            if (ret > 0)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
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
        [Route("orphans/{MotherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int MotherID)
        {
            return await _MotherDBService.GetOrphans(MotherID);
        }
    }
}