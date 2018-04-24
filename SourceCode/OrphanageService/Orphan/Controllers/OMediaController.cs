using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [Authorize]
    [RoutePrefix("api/orphan/media")]
    public class OMediaController : ApiController
    {
        private IOrphanDbService _OrphanDBService;
        private readonly IHttpMessageConfiguerer _httpResponseMessageConfiguerer;

        public OMediaController(IOrphanDbService orphanDBService, IHttpMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _OrphanDBService = orphanDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        #region Face

        [HttpGet]
        [System.Web.Http.Route("face/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("face/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFacePhotoSized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("face/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFacePhotoSizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("face/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanFacePhoto(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanFaceImage(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion Face

        #region BirthCertificate

        [HttpGet]
        [System.Web.Http.Route("birth/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("birth/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificateSized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("birth/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificateSizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("birth/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanBirthCertificate(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanBirthCertificate(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion BirthCertificate

        #region FamilyCardPagePhoto

        [HttpGet]
        [System.Web.Http.Route("familycard/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("familycard/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPageSized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("familycard/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPageSizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("familycard/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanFamilyCardPage(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanFamilyCardPagePhoto(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion FamilyCardPagePhoto

        #region Full

        [HttpGet]
        [System.Web.Http.Route("full/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("full/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFullPhotoSized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("full/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFullPhotoSizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("full/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanFullPhoto(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanFullPhoto(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion Full

        #region EducationCert1

        [HttpGet]
        [System.Web.Http.Route("education/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanCertificate(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("education/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanEducationCertSized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("education/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanEducationCertSizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("education/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanEducationCert(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            ////if (data == null || data.Length == 0)
            ////    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanCertificate(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion EducationCert1

        #region EducationCert2

        [HttpGet]
        [System.Web.Http.Route("education2/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert2(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanCertificate2(Oid);
            return _httpResponseMessageConfiguerer.ImageContent(image);
        }

        [HttpGet]
        [System.Web.Http.Route("education2/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert2Sized(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate2(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("education2/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert2SizedAndCompressed(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate2(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("education2/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanEducationCert2(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanCertificate2(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion EducationCert2

        #region HealthReportFile

        [HttpGet]
        [System.Web.Http.Route("healthreport/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanHealthReport(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanHealthReporte(Oid);
            return _httpResponseMessageConfiguerer.PDFFileContent(image);
        }

        [HttpPut]
        [HttpPost]
        [System.Web.Http.Route("healthreport/{Oid}")]
        public async Task<HttpResponseMessage> setOrphanHealthReport(int Oid)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var data = await _httpResponseMessageConfiguerer.GetMIMIContentData(Request);

            //if (data == null || data.Length == 0)
            //    return _httpResponseMessageConfiguerer.NotAcceptable();

            var Successed = await _OrphanDBService.SetOrphanHealthReporte(Oid, data);

            if (Successed)
                return result;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified);
        }

        #endregion HealthReportFile
    }
}