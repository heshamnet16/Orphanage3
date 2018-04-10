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