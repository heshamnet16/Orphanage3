using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            config.Routes.MapHttpRoute(
                name: "OrphanApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }
    }
}
