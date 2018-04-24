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

namespace OrphanageService.GlobalExceptionsHandlers
{
    public class GlobalExceptionsHandler : IExceptionHandler
    {
        private ILogger _logger = new Logger();

        public async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            ExceptionHandler.ThrowHttpResponseMessage(context.Exception);
        }
    }
}