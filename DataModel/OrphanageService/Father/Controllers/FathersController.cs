using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [RoutePrefix("api/father")]
    public class FathersController : ApiController
    {
        private readonly IFatherDbService _FatherDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;

        public FathersController(IFatherDbService fatherDBService, IHttpMessageConfiguerer httpMessageConfiguerer)
        {
            _FatherDBService = fatherDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
        }

        //api/father/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Father> Get(int id)
        {
            return await _FatherDBService.GetFather(id);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(OrphanageDataModel.Persons.Father father)
        {            
            var ret =  await _FatherDBService.SaveFather(father);
            if(ret > 0)
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
        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> Get(int pageSize, int pageNumber)
        {
            return await _FatherDBService.GetFathers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("orphans/{FatherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FatherID)
        {
            return await _FatherDBService.GetOrphans(FatherID);
        }


    }
}