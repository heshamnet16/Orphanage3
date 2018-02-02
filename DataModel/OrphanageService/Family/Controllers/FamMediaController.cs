using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Family.Controllers
{
    [RoutePrefix("api/family/media")]
    public class FamMediaController : ApiController
    {
        private IFamilyDbService _FamilyDBService;
        private readonly IHttpResponseMessageConfiguerer _httpResponseMessageConfiguerer;

        public FamMediaController(IFamilyDbService familyDBService, IHttpResponseMessageConfiguerer httpResponseMessageConfiguerer)
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

        #endregion FamilyCardPhotoPage2
    }
}