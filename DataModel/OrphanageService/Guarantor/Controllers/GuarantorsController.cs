using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Guarantor.Controllers
{
    [RoutePrefix("api/guarantor")]
    public class GuarantorsController : ApiController
    {
        private readonly IGuarantorDbService _GuarantorDBService;

        public GuarantorsController(IGuarantorDbService guarantorDBService)
        {
            _GuarantorDBService = guarantorDBService;
        }

        //api/guarantor/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<GuarantorDto> Get(int id)
        {
            return await _GuarantorDBService.GetGuarantor(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<GuarantorDto>> Get(int pageSize, int pageNumber)
        {
            return await _GuarantorDBService.GetGuarantors(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetCaregiversCount()
        {
            return await _GuarantorDBService.GetGuarantorsCount();
        }

        [HttpGet]
        [Route("orphans/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanDto>> GetFamilyOrphans(int GId)
        {
            return await _GuarantorDBService.GetOrphans(GId);
        }

        [HttpGet]
        [Route("bails/{GId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<BailDto>> GetBails(int GId)
        {
            return await _GuarantorDBService.GetBails(GId);
        }
    }
}
