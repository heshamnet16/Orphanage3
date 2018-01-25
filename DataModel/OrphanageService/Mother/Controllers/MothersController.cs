using OrphanageService.DataContext.Persons;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Mother.Controllers
{
    [RoutePrefix("api/mother")]
    public class MothersController : ApiController
    {
        private readonly IMotherDbService _MotherDBService;

        public MothersController(IMotherDbService fatherDBService)
        {
            _MotherDBService = fatherDBService;
        }

        //api/mother/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<MotherDC> Get(int id)
        {
            return await _MotherDBService.GetMother(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<MotherDC>> Get(int pageSize, int pageNumber)
        {
            return await _MotherDBService.GetMothers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("orphans/{MotherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanDC>> GetOrphans(int MotherID)
        {
            return await _MotherDBService.GetOrphans(MotherID);
        }
    }
}
