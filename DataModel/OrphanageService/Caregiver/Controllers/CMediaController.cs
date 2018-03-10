using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Caregiver.Controllers
{
    [RoutePrefix("api/caregiver/media")]
    public class CMediaController : ApiController
    {
        private ICaregiverDbService _CaregiverDBService;
        private readonly IHttpMessageConfiguerer _httpResponseMessageConfiguerer;

        public CMediaController(ICaregiverDbService caregiverDBService, IHttpMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _CaregiverDBService = caregiverDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        #region CaregiverIdentityCardPhotoFace

        [HttpGet]
        [System.Web.Http.Route("idface/{CId}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoFace(int CId)
        {
            var image = await _CaregiverDBService.GetIdentityCardFace(CId);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("idface/{CId}/{Size}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoFace(int CId, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _CaregiverDBService.GetIdentityCardFace(CId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("idface/{CId}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoFace(int CId, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _CaregiverDBService.GetIdentityCardFace(CId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [System.Web.Http.Route("idface/{Cid}")]
        public async Task<HttpResponseMessage> SetCaregiverIdentityCardPhotoFace(int Cid)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            //if (data != null)
            //{
                await _CaregiverDBService.SetIdentityCardFace(Cid, data);
                return result;
            //}
            //else
            //{
            //    throw new HttpResponseException(Request.CreateResponse(_httpResponseMessageConfiguerer.NotAcceptable()));
            //}
        }

        #endregion CaregiverIdentityCardPhotoFace

        #region CaregiverIdentityCardPhotoBack

        [HttpGet]
        [System.Web.Http.Route("idback/{CId}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoBack(int CId)
        {
            var image = await _CaregiverDBService.GetIdentityCardBack(CId);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("idback/{CId}/{Size}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoBack(int CId, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _CaregiverDBService.GetIdentityCardBack(CId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("idback/{CId}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getCaregiverIdentityCardPhotoBack(int CId, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _CaregiverDBService.GetIdentityCardBack(CId);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPost]
        [HttpPut]
        [System.Web.Http.Route("idback/{Cid}")]
        public async Task<HttpResponseMessage> SetCaregiverIdentityCardPhotoBack(int Cid)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);
            //if (data != null)
            //{
                await _CaregiverDBService.SetIdentityCardBack(Cid, data);
                return result;
            //}
            //else
            //{
            //    throw new HttpResponseException(Request.CreateResponse(_httpResponseMessageConfiguerer.NotAcceptable()));
            //}
        }

        #endregion CaregiverIdentityCardPhotoBack
    }
}