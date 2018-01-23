using System.Web.Http;
using Unity.AspNet.WebApi;

namespace OrphanageService
{
    public class WepApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            return config;
        }
    }
}
