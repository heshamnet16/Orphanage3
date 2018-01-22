using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.EnsureInitialized();
            appBuilder.UseWebApi(httpConfiguration);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
