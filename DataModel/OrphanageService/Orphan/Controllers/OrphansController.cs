using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan")]
    public class OrphanController : ApiController
    {
        private readonly IOrphanDBService _OrphanDBService;

        public OrphanController(IOrphanDBService orphanDBService)
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
        public async Task<IEnumerable<OrphanDC>> Get(int pageSize,int pageNumber)
        {
            return await _OrphanDBService.GetOrphans(pageSize,pageNumber);
        }

    }
}
