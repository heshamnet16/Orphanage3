using OrphanageService.Services.Interfaces;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrphanageService.Services
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {
        public HttpResponseException HandleValidationException(DbEntityValidationException dbEntityValidation)
        {
            string message = string.Empty;
            foreach (var msg in dbEntityValidation.EntityValidationErrors)
            {
                foreach (var err in msg.ValidationErrors)
                {
                    message += err.ErrorMessage + ":" + err.PropertyName + ";";
                }
            }
            //resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = message
            };
            response.Content = new StringContent(message);
            return new HttpResponseException(response);
        }
    }
}