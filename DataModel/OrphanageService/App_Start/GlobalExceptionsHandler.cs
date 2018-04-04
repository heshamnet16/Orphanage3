using OrphanageService.Services.Exceptions;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace OrphanageService.App_Start
{
    public class GlobalExceptionsHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.Exception is HasForeignKeyException)
            {
                var hasForeignKeyException = (HasForeignKeyException)context.Exception;
                string message = hasForeignKeyException.InnerException.Message;
                var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (context.Exception is DuplicatedObjectException)
            {
                var objectNotFoundException = (DuplicatedObjectException)context.Exception;
                string message = objectNotFoundException.InnerException.Message;
                var response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (context.Exception is ObjectNotFoundException)
            {
                string message = "the object ist not found";
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (context.Exception is DbEntityValidationException)
            {
                var dbEntityValidation = (DbEntityValidationException)context.Exception;
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
                throw new HttpResponseException(response);
            }
            else
                throw context.Exception;
        }
    }
}