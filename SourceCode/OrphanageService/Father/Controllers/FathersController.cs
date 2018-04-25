using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [Authorize(Roles = "Admin, CanRead")]
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
            var ret = await _FatherDBService.GetFather(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [Authorize(Roles = "Admin, CanDelete")]
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(OrphanageDataModel.Persons.Father father)
        {
            var ret = 0;

            ret = await _FatherDBService.SaveFather(father);

            if (ret > 0)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [Authorize(Roles = "Admin, CanDelete")]
        [HttpPut]
        [Route("color")]
        public async Task<HttpResponseMessage> SetFatherColor(int FatherId, int colorValue)
        {
            if (colorValue == -1)
                await _FatherDBService.SetFatherColor(FatherId, null);
            else
                await _FatherDBService.SetFatherColor(FatherId, colorValue);

            return _httpMessageConfiguerer.OK();
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> Get(int pageSize, int pageNumber)
        {
            return await _FatherDBService.GetFathers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetByIds([FromUri] IList<int> fathersIds)
        {
            var ret = await _FatherDBService.GetFathers(fathersIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetFathersCount()
        {
            return await _FatherDBService.GetFathersCount();
        }

        [HttpGet]
        [Route("orphans/{FatherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FatherID)
        {
            return await _FatherDBService.GetOrphans(FatherID);
        }

        [HttpGet]
        [Route("orphans/count/{FatherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount(int FatherID)
        {
            return await _FatherDBService.GetOrphansCount(FatherID);
        }
    }
}