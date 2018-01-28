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
                cfg.CreateMap<OrphanageDataModel.Persons.Orphan, OrphanDto>();
                cfg.CreateMap<OrphanageDataModel.Persons.Caregiver, CaregiverDto>();
                cfg.CreateMap<OrphanageDataModel.Persons.Father, FatherDto>();
                cfg.CreateMap<OrphanageDataModel.Persons.Guarantor, GuarantorDto>();
                cfg.CreateMap<OrphanageDataModel.Persons.Mother, MotherDto>();
                cfg.CreateMap<OrphanageDataModel.Persons.User, UserDto>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Account, AccountDto>();
                cfg.CreateMap<OrphanageDataModel.FinancialData.Bail, BailDto>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Address, AddressDto>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Family, FamilyDto>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Health, HealthDto>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Name, NameDto>();
                cfg.CreateMap<OrphanageDataModel.RegularData.Study, StudyDto>();
            });
        }
    }
}
