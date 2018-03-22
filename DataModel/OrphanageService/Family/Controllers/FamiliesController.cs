using OrphanageService.Filters;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Family.Controllers
{
    [RoutePrefix("api/family")]
    public class FamiliesController : ApiController
    {
        private readonly IFamilyDbService _FamilyDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;
        private readonly IExceptionHandlerService _exceptionHandlerService;

        public FamiliesController(IFamilyDbService familyDBService, IHttpMessageConfiguerer httpMessageConfiguerer, IExceptionHandlerService exceptionHandlerService)
        {
            _FamilyDBService = familyDBService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
            _exceptionHandlerService = exceptionHandlerService;
        }

        //api/family/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.RegularData.Family> Get(int id)
        {
            var ret = await _FamilyDBService.GetFamily(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(OrphanageDataModel.RegularData.Family family)
        {
            var ret = false;
            try
            {
                ret = await _FamilyDBService.SaveFamily(family);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(OrphanageDataModel.RegularData.Family family)
        {
            var ret = 0;
            try
            {
                ret = await _FamilyDBService.AddFamily(family);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            if (ret > 0)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, ret);
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpDelete]
        [Route("{FamID}")]
        public async Task<HttpResponseMessage> Delete(int FamID)
        {
            var ret = await _FamilyDBService.DeleteFamily(FamID);
            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetByIds([FromUri] IList<int> familiesIds)
        {
            var ret = await _FamilyDBService.GetFamilies(familiesIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> Get(int pageSize, int pageNumber)
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
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetFamilyOrphans(int famId)
        {
            return await _FamilyDBService.GetOrphans(famId);
        }

        [HttpGet]
        [Route("orphans/count/{FamId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount(int FamId)
        {
            return await _FamilyDBService.GetOrphansCount(FamId);
        }
    }
}