using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Family.Controllers
{
    [RoutePrefix("api/family")]
    public class FamiliesController : ApiController
    {
        private readonly IFamilyDbService _FamilyDBService;

        public FamiliesController(IFamilyDbService familyDBService)
        {
            _FamilyDBService = familyDBService;
        }

        //api/family/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<FamilyDC> Get(int id)
        {
            return await _FamilyDBService.GetFamily(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<FamilyDC>> Get(int pageSize, int pageNumber)
        {
            return await _FamilyDBService.GetFamilies(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetFamiliesCount()
        {
            return await _FamilyDBService.GetFamiliesCount();
        }

        [HttpGet]
        [Route("orphans/{FamId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IList<OrphanDC>> GetFamilyOrphans(int famId)
        {
            return await _FamilyDBService.GetOrphans(famId);
        }
    }
}
