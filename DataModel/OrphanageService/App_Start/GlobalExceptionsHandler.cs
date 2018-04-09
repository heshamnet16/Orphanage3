using OrphanageService.Services;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using System;
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
        private ILogger _logger = new Logger();

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            logException(context.Exception);
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

        private void logException(Exception exc)
        {
            if (exc == null) return;
            _logger.Error($"an Exception has occurred with exception message: ({exc.Message}) and exception type {exc.GetType().ToString()}");
            if (exc.InnerException != null)
            {
                logException(exc.InnerException.InnerException);
            }
        }
    }
}