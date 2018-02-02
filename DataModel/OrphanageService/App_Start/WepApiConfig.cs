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

            SwaggerConfig.Register(config);
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{namespace}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //        name: "DefaultApiPaging",
            //        routeTemplate: "api/{namespace}/{controller}/{pageSize}/{pageNumber}",
            //        defaults: new { id = RouteParameter.Optional }
            //    );

            config.MapHttpAttributeRoutes();

            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            return config;
        }
    }
}