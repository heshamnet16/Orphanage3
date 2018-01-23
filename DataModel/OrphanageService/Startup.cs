using Owin;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.EnsureInitialized();
            appBuilder.UseWebApi(httpConfiguration);
            
        }
    }
}
