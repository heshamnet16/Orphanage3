﻿using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.DataBank.Controllers
{
    [Authorize(Roles = "Admin, CanRead")]
    [RoutePrefix("api/databank")]
    public class DataBanksController : ApiController
    {
        private readonly IRegularDataService _regularDataService;
        private readonly ISQLStatmentsExecuter _sQLStatmentsExecuter;

        public DataBanksController(IRegularDataService regularDataService, ISQLStatmentsExecuter sQLStatmentsExecuter)
        {
            _regularDataService = regularDataService;
            _sQLStatmentsExecuter = sQLStatmentsExecuter;
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

        //api/databse/script/executer
        [HttpPut]
        [Route("script/executer")]
        [Authorize(Roles = "Admin")]
        public async Task<int> ExecuteScript(string scripts)
        {
            var ret = await _sQLStatmentsExecuter.ExecuteCommands(scripts);
            return ret;
        }
    }
}