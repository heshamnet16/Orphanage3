using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [RoutePrefix("api/father")]
    public class FathersController : ApiController
    {
        private readonly IFatherDbService _FatherDBService;

        public FathersController(IFatherDbService fatherDBService)
        {
            _FatherDBService = fatherDBService;
        }

        //api/father/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Father> Get(int id)
        {
            return await _FatherDBService.GetFather(id);
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