using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [RoutePrefix("api/father/media")]
    public class FMediaController : ApiController
    {
        private IFatherDbService _FatherDBService;
        private readonly IHttpResponseMessageConfiguerer _httpResponseMessageConfiguerer;

        public FMediaController(IFatherDbService fatherDBService, IHttpResponseMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _FatherDBService = fatherDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        #region PersonalPhoto
        [HttpGet]
        [System.Web.Http.Route("photo/{Fid}")]
        public async Task<HttpResponseMessage> getFatherFacePhoto(int Fid)
        {
            var image = await _FatherDBService.GetFatherPhoto(Fid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("photo/{Fid}/{Size}")]
        public async Task<HttpResponseMessage> getFatherFacePhoto(int Fid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherPhoto(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("photo/{Fid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getFatherFacePhoto(int Fid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherPhoto(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion
        #region DeathCertificate
        [HttpGet]
        [System.Web.Http.Route("death/{Fid}")]
        public async Task<HttpResponseMessage> GetFatherDeathCertificate(int Fid)
        {
            var image = await _FatherDBService.GetFatherDeathCertificate(Fid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("death/{Fid}/{Size}")]
        public async Task<HttpResponseMessage> GetFatherDeathCertificate(int Fid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherDeathCertificate(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("death/{Fid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> GetFatherDeathCertificate(int Fid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherDeathCertificate(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion

    }
}
