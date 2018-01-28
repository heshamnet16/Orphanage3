using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Bail.Controllers
{
    [RoutePrefix("api/bail")]
    public class BailsController : ApiController
    {
        private IBailDbService _bailDbService;

        public BailsController(IBailDbService bailDbService)
        {
            _bailDbService = bailDbService;
        }

        //api/caregiver/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<BailDto> Get(int id)
        {
            return await _bailDbService.GetBailDC(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<BailDto>> Get(int pageSize, int pageNumber)
        {
            return await _bailDbService.GetBails(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetBailsCount()
        {
            return await _bailDbService.GetBailsCount();
        }

        [HttpGet]
        [Route("orphans/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanDto>> GetOrphans(int BId)
        {
            return await _bailDbService.GetOrphans(BId);
        }

        [HttpGet]
        [Route("families/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<FamilyDto>> GetFamilies(int BId)
        {
            return await _bailDbService.GetFamilies(BId);
        }
    }
}
