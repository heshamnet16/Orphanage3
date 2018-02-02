using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Mother.Controllers
{
    [RoutePrefix("api/mother/media")]
    public class MMediaController : ApiController
    {
        private IMotherDbService _MotherDBService;
        private readonly IHttpResponseMessageConfiguerer _httpResponseMessageConfiguerer;

        public MMediaController(IMotherDbService motherDBService, IHttpResponseMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _MotherDBService = motherDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        #region IdCardPhotoFront

        [HttpGet]
        [System.Web.Http.Route("idface/{Mid}")]
        public async Task<HttpResponseMessage> GetMotherIdPhotoFace(int Mid)
        {
            var image = await _MotherDBService.GetMotherIdPhotoFace(Mid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("idface/{Mid}/{Size}")]
        public async Task<HttpResponseMessage> getFatherFacePhoto(int Mid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _MotherDBService.GetMotherIdPhotoFace(Mid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("idface/{Mid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> GetMotherIdPhotoFace(int Mid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _MotherDBService.GetMotherIdPhotoFace(Mid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        #endregion IdCardPhotoFront

        #region IdCardPhotoBack

        [HttpGet]
        [System.Web.Http.Route("idback/{Mid}")]
        public async Task<HttpResponseMessage> GetMotherIdPhotoBack(int Mid)
        {
            var image = await _MotherDBService.GetMotherIdPhotoBack(Mid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("idback/{Mid}/{Size}")]
        public async Task<HttpResponseMessage> GetMotherIdPhotoBack(int Mid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _MotherDBService.GetMotherIdPhotoBack(Mid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("idback/{Mid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> GetMotherIdPhotoBack(int Mid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _MotherDBService.GetMotherIdPhotoBack(Mid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        #endregion IdCardPhotoBack
    }
}