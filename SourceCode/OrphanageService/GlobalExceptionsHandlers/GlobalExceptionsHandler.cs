using OrphanageService.Services;
using OrphanageService.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace OrphanageService.GlobalExceptionsHandlers
{
    public class GlobalExceptionsHandler : IExceptionHandler
    {
        private ILogger _logger = new Logger();

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            ExceptionHandler.ThrowHttpResponseMessage(context.Exception);
            throw context.Exception;
        }
    }
}