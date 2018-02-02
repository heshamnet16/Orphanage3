using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Father.Controllers
{
    [RoutePrefix("api/father/media")]
    public class FMediaController : ApiController
    {
        private IFatherDbService _FatherDBService;
        private readonly IHttpMessageConfiguerer _httpResponseMessageConfiguerer;

        public FMediaController(IFatherDbService fatherDBService, IHttpMessageConfiguerer httpResponseMessageConfiguerer)
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
        public async Task<HttpResponseMessage> getFatherFacePhoto(int Fid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherPhoto(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [Route("photo/{Fid}")]
        public async Task<HttpResponseMessage> SetFatherFacePhoto(int Fid)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            if (data != null)
            {
                await _FatherDBService.SetFatherPhoto(Fid, data);
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }

        }
        #endregion PersonalPhoto

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
        public async Task<HttpResponseMessage> GetFatherDeathCertificate(int Fid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _FatherDBService.GetFatherDeathCertificate(Fid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [Route("death/{Fid}")]
        public async Task<HttpResponseMessage> SetFatherDeathCertificate(int Fid)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            if (data != null)
            {
                await _FatherDBService.SetFatherDeathCertificate(Fid, data);
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }

        }
        #endregion DeathCertificate
    }
}