using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Database.Controllers
{
    [RoutePrefix("api/database")]
    public class DataBankController : ApiController
    {
        private readonly IRegularDataService _regularDataService;

        public DataBankController (IRegularDataService regularDataService)
        {
            _regularDataService = regularDataService;
        }

        //api/databse/clean/addresses
        [HttpGet]
        [Route("clean/addresses")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Address>> CleanAddresses()
        {
            var ret = await _regularDataService.CleanAddresses();
            return ret;
        }

        //api/databse/clean/names
        [HttpGet]
        [Route("clean/names")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Name>> CleanNames()
        {
            var ret = await _regularDataService.CleanNames();
            return ret;
        }

        //api/databse/clean/Studies
        [HttpGet]
        [Route("clean/studies")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Study>> CleanStudies()
        {
            var ret = await _regularDataService.CleanStudies();
            return ret;
        }

        //api/databse/clean/Healths
        [HttpGet]
        [Route("clean/healths")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Health>> CleanHealths()
        {
            var ret = await _regularDataService.CleanHealthies();
            return ret;
        }

    }
}
