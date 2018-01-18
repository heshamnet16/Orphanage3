using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using System.Net.Http;
using System.Net;
using OrphanageService.Utilities;
using OrphanageService.Services;
using OrphanageService.Services.Interfaces;

namespace OrphanageService.Orphan.Controllers
{
    [RoutePrefix("api/orphan")]
    public class MediaController : ApiController
    {
        private IOrphanDBService _OrphanDBService;

        public MediaController(IOrphanDBService orphanDBService)
        {
            _OrphanDBService = orphanDBService;
        }

        #region Face
        [System.Web.Http.Route("media/face/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            return ImageAsHttpResponse(image);
        }

        [System.Web.Http.Route("media/face/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return ImageAsHttpResponse(thumb);
        }

        [System.Web.Http.Route("media/face/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFacePhoto(int Oid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFaceImage(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return ImageAsHttpResponse(thumb);
        }
        #endregion
        #region BirthCertificate
        [System.Web.Http.Route("media/birth/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            return ImageAsHttpResponse(image);
        }

        [System.Web.Http.Route("media/birth/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return ImageAsHttpResponse(thumb);
        }

        [System.Web.Http.Route("media/birth/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanBirthCertificate(int Oid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanBirthCertificate(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return ImageAsHttpResponse(thumb);
        }
        #endregion
        #region FamilyCardPagePhoto
        [System.Web.Http.Route("media/familycard/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            return ImageAsHttpResponse(image);
        }

        [System.Web.Http.Route("media/familycard/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return ImageAsHttpResponse(thumb);
        }

        [System.Web.Http.Route("media/familycard/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFamilyCardPage(int Oid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFamilyCardPagePhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return ImageAsHttpResponse(thumb);
        }
        #endregion
        #region Full
        [System.Web.Http.Route("media/full/{Oid}")]
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid)
        {
            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            return ImageAsHttpResponse(image);
        }

        [System.Web.Http.Route("media/full/{Oid}/{Size}")]
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid, string Size)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]));
            return ImageAsHttpResponse(thumb);
        }

        [System.Web.Http.Route("media/full/{Oid}/{Size}/{Compertion}")]
        public async Task<HttpResponseMessage> getOrphanFullPhoto(int Oid, string Size, int compertion)
        {
            string[] sizeString = Size.Split('x');

            var image = await _OrphanDBService.GetOrphanFullPhoto(Oid);
            var thumb = ImageAdapter.Resize(image, int.Parse(sizeString[0]), int.Parse(sizeString[1]), compertion);
            return ImageAsHttpResponse(thumb);
        }
        #endregion
        private HttpResponseMessage ImageAsHttpResponse(byte[] img)
        {
            if(img == null)
            {
                HttpResponseMessage response1 = new HttpResponseMessage(HttpStatusCode.NoContent);
                return response1;
            }
            MemoryStream ms = new MemoryStream(img);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
    }
}
