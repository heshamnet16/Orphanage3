using AutoMapper;
using Microsoft.Owin.Hosting;
using System;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string baseUrl = Properties.Settings.Default.BaseURI;

            WebApp.Start<Startup>(baseUrl);
            ConfigureMapper();
            Console.WriteLine("Orphan Service is started on port 1515");
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
    }
}