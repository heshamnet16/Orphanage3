﻿using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Utilities
{
    public class HttpResponseMessageConfiguerer : IHttpResponseMessageConfiguerer
    {
        public HttpResponseMessage Created()
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage ImageContent(byte[] img)
        {
            var response = createContentMessage(img);
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
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
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
    }
}
