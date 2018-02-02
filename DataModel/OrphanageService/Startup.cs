using OrphanageService.App_Start;
using Owin;
using System.Web.Http.Dispatcher;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new HttpAreaSelector(httpConfiguration));
            httpConfiguration.EnsureInitialized();
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}