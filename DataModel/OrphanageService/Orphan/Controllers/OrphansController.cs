using Newtonsoft.Json;
using OrphanageService.Filters;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan")]
    public class OrphansController : ApiController
    {
        private readonly IOrphanDbService _OrphanDBService;
        private readonly IHttpMessageConfiguerer _httpMessageConfigurere;
        private readonly IExceptionHandlerService _exceptionHandlerService;

        public OrphansController(IOrphanDbService orphanDBService, IHttpMessageConfiguerer httpMessageConfigurere, IExceptionHandlerService exceptionHandlerService)
        {
            _OrphanDBService = orphanDBService;
            _httpMessageConfigurere = httpMessageConfigurere;
            _exceptionHandlerService = exceptionHandlerService;
        }

        //api/Orphan/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.Orphan> Get(int id)
        {
            var ret = await _OrphanDBService.GetOrphan(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("byIds")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetByIds([FromUri] IList<int> OrphanIds)
        {
            var ret = await _OrphanDBService.GetOrphans(OrphanIds);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> Get(int pageSize, int pageNumber)
        {
            return await _OrphanDBService.GetOrphans(pageSize, pageNumber);
        }

        [HttpGet]
        [Route("excluded")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetExcluded()
        {
            return await _OrphanDBService.GetExcludedOrphans();
        }

        [HttpGet]
        [Route("count")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetOrphansCount()
        {
            return await _OrphanDBService.GetOrphansCount();
        }

        [HttpGet]
        [Route("brothers/{Oid}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetBrothers(int Oid)
        {
            return await _OrphanDBService.GetBrothers(Oid);
        }

        [HttpGet]
        [Route("brothers/count/{Oid}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<int> GetBrothersCount(int Oid)
        {
            return await _OrphanDBService.GetBrothersCount(Oid);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(object orphan)
        {
            var orp = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Orphan>(orphan.ToString());
            OrphanageDataModel.Persons.Orphan ret = null;
            try
            {
                ret = await _OrphanDBService.AddOrphan(orp);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            catch (DuplicatedObjectException dubExc)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, dubExc.InnerException.Message);
            }

            if (ret != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, ret);
            }
            else
            {
                return _httpMessageConfigurere.OK();
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object orphan)
        {
            var orp = JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Orphan>(orphan.ToString());
            var ret = false;
            try
            {
                ret = await _OrphanDBService.SaveOrphan(orp);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            if (ret)
            {
                return _httpMessageConfigurere.OK();
            }
            else
            {
                return _httpMessageConfigurere.NothingChanged();
            }
        }

        [HttpPut]
        [Route("color")]
        public async Task<HttpResponseMessage> SetOrphanColor(int orphanId, int colorValue)
        {
            try
            {
                if (colorValue == -1)
                    await _OrphanDBService.SetOrphanColor(orphanId, null);
                else
                    await _OrphanDBService.SetOrphanColor(orphanId, colorValue);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            return _httpMessageConfigurere.OK();
        }

        [HttpPut]
        [Route("exclude")]
        public async Task<HttpResponseMessage> SetOrphanExclude(int orphanId, bool value)
        {
            try
            {
                await _OrphanDBService.SetOrphanExclude(orphanId, value);
            }
            catch (DbEntityValidationException excp)
            {
                throw _exceptionHandlerService.HandleValidationException(excp);
            }
            return _httpMessageConfigurere.OK();
        }

        [HttpDelete]
        [Route("{Oid}")]
        public async Task<HttpResponseMessage> Delete(int Oid)
        {
            var ret = await _OrphanDBService.DeleteOrphan(Oid);
            if (ret)
            {
                return _httpMessageConfigurere.OK();
            }
            else
            {
                return _httpMessageConfigurere.NothingChanged();
            }
        }
    }
}