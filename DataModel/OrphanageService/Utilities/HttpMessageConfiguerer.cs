using OrphanageService.Utilities.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrphanageService.Utilities
{
    public class HttpMessageConfiguerer : IHttpMessageConfiguerer
    {
        public async Task<HttpResponseMessage> Created(int Id)
        {
            var respMessage =  new HttpResponseMessage(HttpStatusCode.Created);
            using (MemoryStream mem = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(mem))
                {
                    await streamWriter.WriteAsync(Id.ToString());
                    respMessage.Content = new StreamContent(mem);
                    respMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return respMessage;
                }
            }
            
        }

        public async Task<byte[]> GetMIMIContentData(HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage.Content.IsMimeMultipartContent())
            {
                var provider = await httpRequestMessage.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
                
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = await content.ReadAsStreamAsync();
                        MemoryStream mem = new MemoryStream();
                        await stream.CopyToAsync(mem);
                        return mem.ToArray();
                    }
            }
            else
            {
                return null;
            }
            return null;
        }

        public HttpResponseMessage ImageContent(byte[] img)
        {
            var response = createContentMessage(img);
            if (response.StatusCode != HttpStatusCode.NoContent)
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
        }

        public HttpResponseMessage NoContent()
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage PDFFileContent(byte[] pdfFile)
        {
            var response = createContentMessage(pdfFile);
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "OrphangePDF.pdf";
            if (response.StatusCode != HttpStatusCode.NoContent)
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

        public HttpResponseMessage NotAcceptable()
        {
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }
        private HttpResponseMessage createContentMessage(byte[] data)
        {
            if (data == null)
            {
                HttpResponseMessage response1 = new HttpResponseMessage(HttpStatusCode.NoContent);
                return response1;
            }
            MemoryStream ms = new MemoryStream(data);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            return response;
        }

        public HttpResponseMessage NothingChanged()
        {
            return new HttpResponseMessage(HttpStatusCode.NotModified);
        }

        public HttpResponseMessage OK()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}