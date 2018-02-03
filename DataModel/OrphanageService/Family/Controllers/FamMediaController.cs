using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Family.Controllers
{
    [RoutePrefix("api/family/media")]
    public class FamMediaController : ApiController
    {
        private IFamilyDbService _FamilyDBService;
        private readonly IHttpMessageConfiguerer _httpResponseMessageConfiguerer;

        public FamMediaController(IFamilyDbService familyDBService, IHttpMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _FamilyDBService = familyDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        #region FamilyCardPhotoPage1

        [HttpGet]
        [System.Web.Http.Route("page1/{FamId}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage1(int FamId)
        {
            var image = await _FamilyDBService.GetFamilyCardPage1(FamId);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("page1/{FamId}/{Size}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage1(int FamId, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FamilyDBService.GetFamilyCardPage1(FamId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("page1/{FamId}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage1(int FamId, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FamilyDBService.GetFamilyCardPage1(FamId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [Route("page1/{FamId}")]
        public async Task<HttpResponseMessage> SetFamilyCardPhotoPage1(int FamId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            if (data != null)
            {
                await _FamilyDBService.SetFamilyCardPage1(FamId, data);
                return result;
            }
            else
            {
                throw new HttpResponseException(_httpResponseMessageConfiguerer.NotAcceptable());
            }

        }
        #endregion FamilyCardPhotoPage1

        #region FamilyCardPhotoPage2

        [HttpGet]
        [System.Web.Http.Route("page2/{FamId}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage2(int FamId)
        {
            var image = await _FamilyDBService.GetFamilyCardPage2(FamId);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("page2/{FamId}/{Size}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage2(int FamId, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FamilyDBService.GetFamilyCardPage2(FamId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("page2/{FamId}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getFamilyCardPhotoPage2(int FamId, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FamilyDBService.GetFamilyCardPage2(FamId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [Route("page2/{FamId}")]
        public async Task<HttpResponseMessage> SetFamilyCardPhotoPage2(int FamId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            if (data != null)
            {
                await _FamilyDBService.SetFamilyCardPage2(FamId, data);
                return result;
            }
            else
            {
                throw new HttpResponseException(_httpResponseMessageConfiguerer.NotAcceptable());
            }

        }

        #endregion FamilyCardPhotoPage2
    }
}