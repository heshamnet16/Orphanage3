﻿using AutoMapper;
using Microsoft.Owin.Hosting;
using OrphanageService.Services.Interfaces;
using System;
using System.ServiceProcess;
using Unity;

namespace OrphanageService
{
    partial class SelfHostServiceBase : ServiceBase
    {
        private IDisposable _webapp;
        private ILogger _logger;

        public SelfHostServiceBase()
        {
            InitializeComponent();
            _logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Information("trying to configure mapper");
            ConfigureMapper();
            string baseUrl = Properties.Settings.Default.BaseURI;
            _webapp = WebApp.Start<Startup>(baseUrl);
            _logger.Information("Orphan Service is started on port 1515");
            Console.ReadLine();
        }

        /// <summary>
        /// configure the mapper between the DataModel and the DataContract
        /// </summary>
        private static void ConfigureMapper()
        {            
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<OrphanageDataModel.Persons.Orphan, OrphanageDataModel.Persons.Orphan>();
                cfg.CreateMap<OrphanageDataModel.Persons.Caregiver, OrphanageDataModel.Persons.Caregiver>();
                cfg.CreateMap<OrphanageDataModel.Persons.Father, OrphanageDataModel.Persons.Father>();
                cfg.CreateMap<OrphanageDataModel.Persons.Guarantor, OrphanageDataModel.Persons.Guarantor>();
                cfg.CreateMap<OrphanageDataModel.Persons.Mother, OrphanageDataModel.Persons.Mother>();
                cfg.CreateMap<OrphanageDataModel.Persons.User, OrphanageDataModel.Persons.User>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Account, OrphanageDataModel.FinancialData.Account>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Bail, OrphanageDataModel.FinancialData.Bail>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Address, OrphanageDataModel.RegularData.Address>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Family, OrphanageDataModel.RegularData.Family>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Health, OrphanageDataModel.RegularData.Health>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Name, OrphanageDataModel.RegularData.Name>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Study, OrphanageDataModel.RegularData.Study>();
            });
        }

        protected override void OnStop()
        {
            _webapp?.Dispose();
        }
    }
}