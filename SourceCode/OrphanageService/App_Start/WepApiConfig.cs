using System.Web.Http;
using Unity.AspNet.WebApi;

namespace OrphanageService
{
    public class WepApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{namespace}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //        name: "DefaultApiPaging",
            //        routeTemplate: "api/{namespace}/{controller}/{pageSize}/{pageNumber}",
            //        defaults: new { id = RouteParameter.Optional }
            //    );

            SwaggerConfig.Register(config);
            return config;
        }
    }
}