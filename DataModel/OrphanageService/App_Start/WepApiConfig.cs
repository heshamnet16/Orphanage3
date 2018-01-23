using System.Web.Http;
using Unity.AspNet.WebApi;

namespace OrphanageService
{
    public class WepApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            //config.Routes.MapHttpRoute(
            //    name: "OrphanApi",
            //    routeTemplate: "api/orphan/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "FatherApi",
            //    routeTemplate: "api/father/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            return config;
        }
    }
}
