using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan/media")]
    public class OMediaController : ApiController
    {
        private IOrphanDbService _OrphanDBService;
        private readonly IHttpResponseMessageConfiguerer _httpResponseMessageConfiguerer;

        public OMediaController(IOrphanDbService orphanDBService, IHttpResponseMessageConfiguerer httpResponseMessageConfiguerer)
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
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("face/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion
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
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("birth/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion
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
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("familycard/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion
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
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("full/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion

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
        public async Task<HttpResponseMessage> getOrphanEducationCert(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("education/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion

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
        public async Task<HttpResponseMessage> getOrphanEducationCert2(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate2(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }

        [HttpGet]
        [System.Web.Http.Route("education2/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanEducationCert2(int Oid, string Size, long compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanCertificate2(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return _httpResponseMessageConfiguerer.ImageContent(thumb);
        }
        #endregion

        #region HealthReportFile
        [HttpGet]
        [System.Web.Http.Route("healthreport/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanHealthReport(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanHealthReporte(Oid);
            return _httpResponseMessageConfiguerer.PDFFileContent(image);
        }
        #endregion

    }
}
