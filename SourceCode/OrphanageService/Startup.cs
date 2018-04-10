using Owin;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new HttpAreaSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionsHandler());
            httpConfiguration.EnsureInitialized();
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}