using AutoMapper;
using Microsoft.Owin.Hosting;
using OrphanageService.Services;
using System;
using W.Firewall;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CreateFirewallRules();
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

        private static void CreateFirewallRules()
        {
            try
            {
                if (!Rules.Exists("Orphanage3"))
                {
                    Rules.Add("Orphanage3", "", localPorts: "1515");
                }
            }
            catch (Exception ex)
            {
                var logger = new Logger();
                logger.Fatal("Connot create Orphanage3 rule");
                logger.Fatal(ex.Message);
                if (ex.InnerException != null)
                {
                    logger.Fatal(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Fatal(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {
                            logger.Fatal(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
            }
        }
    }
}