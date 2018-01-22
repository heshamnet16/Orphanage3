using OrphanageService.DataContext.Persons;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [RoutePrefix("api/father")]
    public class FathersController :ApiController
    {
        private readonly IFatherDBService _FatherDBService;

        public FathersController(IFatherDBService fatherDBService)
        {
            _FatherDBService = fatherDBService;
        }

        //api/Orphan/{id}
        [Route("{id}")]
        public async Task<FatherDC> Get(int id)
        {
            return await _FatherDBService.GetFather(id);
        }

        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<FatherDC>> Get(int pageSize, int pageNumber)
        {
            return await _FatherDBService.GetFathers(pageSize, pageNumber);
        }

        [Route("orphans/{FatherID}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanDC>> GetOrphans(int FatherID)
        {
            return await _FatherDBService.GetOrphans(FatherID);
        }
    }
}
