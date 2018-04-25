using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace OrphanageService.GlobalExceptionsHandlers
{
    public static class ExceptionHandler
    {
        private static ILogger _logger = null;

        static ExceptionHandler()
        {
            _logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
        }

        private static void logException(Exception exc)
        {
            if (exc == null) return;
            _logger.Error($"an Exception has occurred with exception message: ({exc.Message}) and exception type {exc.GetType().ToString()}");
            if (exc.InnerException != null)
            {
                logException(exc.InnerException.InnerException);
            }
        }

        public static void ThrowHttpResponseMessage(Exception exception)
        {
            logException(exception);
            if (exception is HasForeignKeyException)
            {
                var hasForeignKeyException = (HasForeignKeyException)exception;
                string message = hasForeignKeyException.InnerException.Message;
                var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (exception is DuplicatedObjectException)
            {
                var objectNotFoundException = (DuplicatedObjectException)exception;
                string message = objectNotFoundException.InnerException.Message;
                var response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (exception is ObjectNotFoundException)
            {
                string message = "the object ist not found";
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else if (exception is DbEntityValidationException)
            {
                var dbEntityValidation = (DbEntityValidationException)exception;
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
            else if (exception is AuthenticationException)
            {
                string message = exception.Message;
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = message
                };
                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
            else
                throw exception;
        }
    }
}