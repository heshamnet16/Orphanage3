using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Caregiver.Controllers
{
    [RoutePrefix("api/caregiver")]
    public class CaregiversController : ApiController
    {
        private readonly ICaregiverDbService _CaregiverDBService;

        public CaregiversController(ICaregiverDbService caregiverDBService)
        {
            _CaregiverDBService = caregiverDBService;
        }

        //api/caregiver/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Caregiver> Get(int id)
        {
            return await _CaregiverDBService.GetCaregiver(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> Get(int pageSize, int pageNumber)
        {
            return await _CaregiverDBService.GetCaregivers(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetCaregiversCount()
        {
            return await _CaregiverDBService.GetCaregiversCount();
        }

        [HttpGet]
        [Route("orphans/{CId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetFamilyOrphans(int CId)
        {
            return await _CaregiverDBService.GetOrphans(CId);
        }
    }
}
