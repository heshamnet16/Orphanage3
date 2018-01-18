using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan")]
    public class OrphanController : ApiController
    {       
        //api/Orphan/{id}
        [Route("{id}")]
        public async Task<OrphanDC> Get(int id)
        {
            return await DBService.GetOrphan(id);
        }

        [Route("{pageSize}/{pageNumber}")]
        public async Task<IEnumerable<OrphanDC>> Get(int pageSize,int pageNumber)
        {
            return await DBService.GetOrphans(pageSize,pageNumber);
        }

    }
}
