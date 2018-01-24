using OrphanageService.DataContext.Persons;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan")]
    public class OrphansController : ApiController
    {
        private readonly IOrphanDBService _OrphanDBService;

        public OrphansController(IOrphanDBService orphanDBService)
        {
            _OrphanDBService = orphanDBService;
        }

        //api/Orphan/{id}
        [Route("{id}")]
        public async Task<OrphanDC> Get(int id)
        {
            return await _OrphanDBService.GetOrphan(id);
        }

        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanDC>> Get(int pageSize,int pageNumber)
        {
            return await _OrphanDBService.GetOrphans(pageSize,pageNumber);
        }

    }
}
