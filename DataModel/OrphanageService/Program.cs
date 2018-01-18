using AutoMapper;
using Microsoft.Owin.Hosting;
using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System;

namespace OrphanageService
{
    public class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "http://localhost:1515/";

            ConfigureMapper();

            WebApp.Start<Startup>(baseUrl);

            Console.WriteLine("Orphan Service is started on port 1515");
            Console.ReadLine();
        }
        /// <summary>
        /// configure the mapper between the DataModel and the DataContract
        /// </summary>
        static void ConfigureMapper()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<OrphanageDataModel.Persons.Orphan, OrphanDC>();
                cfg.CreateMap<OrphanageDataModel.Persons.Caregiver, CaregiverDC>();
                cfg.CreateMap<OrphanageDataModel.Persons.Father, FatherDC>();
                cfg.CreateMap<OrphanageDataModel.Persons.Guarantor, GuarantorDC>();
                cfg.CreateMap<OrphanageDataModel.Persons.Mother, MotherDC>();
                cfg.CreateMap<OrphanageDataModel.Persons.User, UserDC>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Account, AccountDC>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Bail, BailDC>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Address, AddressDC>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Family, FamilyDC>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Health, HealthDC>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Name, NameDC>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Study, StudyDC>();
            });
        }
    }
}
