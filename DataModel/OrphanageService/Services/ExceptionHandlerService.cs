using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {
        public HttpResponseMessage HandleValidationException(DbEntityValidationException dbEntityValidation)
        {
            string message = string.Empty;
            foreach (var msg in dbEntityValidation.EntityValidationErrors)
            {
                foreach (var err in msg.ValidationErrors)
                {
                    message += err.ErrorMessage + "\\\\\\" + err.PropertyName + "\n";
                }
            }
            var resp = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            using (MemoryStream mem = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(mem))
                {
                    streamWriter.Write(message);
                    resp.Content = new StreamContent(mem);
                    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return resp;
                }
            }
        }
    }
}
