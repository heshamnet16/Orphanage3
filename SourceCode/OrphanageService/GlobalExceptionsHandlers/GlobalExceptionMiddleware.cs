using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace OrphanageService.GlobalExceptionsHandlers
{
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ThrowHttpResponseMessage(ex);
            }
        }
    }
}