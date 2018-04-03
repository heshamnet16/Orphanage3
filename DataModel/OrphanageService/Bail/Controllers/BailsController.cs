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

namespace OrphanageService.Bail.Controllers
{
    [RoutePrefix("api/bail")]
    public class BailsController : ApiController
    {
        private IBailDbService _bailDbService;
        private readonly IHttpMessageConfiguerer _httpMessageConfiguerer;
        private readonly IExceptionHandlerService _exceptionHandlerService;

        public BailsController(IBailDbService bailDbService, IHttpMessageConfiguerer httpMessageConfiguerer, IExceptionHandlerService exceptionHandlerService)
        {
            _bailDbService = bailDbService;
            _httpMessageConfiguerer = httpMessageConfiguerer;
            _exceptionHandlerService = exceptionHandlerService;
        }

        //api/bail/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.FinancialData.Bail> Get(int id)
        {
            var ret = await _bailDbService.GetBailDC(id);
            if (ret == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            else
                return ret;
        }

        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> Get(int pageSize, int pageNumber)
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
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int BId)
        {
            return await _bailDbService.GetOrphans(BId);
        }

        [HttpGet]
        [Route("families/{BId}")]
        [CacheFilter(TimeDuration = 200)]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int BId)
        {
            return await _bailDbService.GetFamilies(BId);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put(object bail)
        {
            var accountEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Bail>(bail.ToString());
            var ret = false;
            try
            {
                ret = await _bailDbService.SaveBail(accountEntity);
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
        public async Task<HttpResponseMessage> Post(object bail)
        {
            var bailEntity = JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Bail>(bail.ToString());
            OrphanageDataModel.FinancialData.Bail ret = null;
            try
            {
                ret = await _bailDbService.AddBail(bailEntity);
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
                return _httpMessageConfiguerer.NothingChanged();
            }
        }

        [HttpDelete]
        [Route("{CID}/{forceDelete}")]
        public async Task<HttpResponseMessage> Delete(int CID, bool forceDelete)
        {
            var ret = await _bailDbService.DeleteBail(CID, forceDelete);
            if (ret)
            {
                return _httpMessageConfiguerer.OK();
            }
            else
            {
                return _httpMessageConfiguerer.NothingChanged();
            }
        }
    }
}