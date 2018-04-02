using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Info.Controllers
{
    [RoutePrefix("api/info")]
    public class InfosController : ApiController
    {
        //api/info/verison
        [HttpGet]
        [Route("version")]
        public string GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}