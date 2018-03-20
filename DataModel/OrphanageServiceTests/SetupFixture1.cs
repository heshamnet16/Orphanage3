﻿using AutoMapper;
using NUnit.Framework;
using OrphanageService.Services;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System;
using Unity;

namespace OrphanageServiceTests
{
    [SetUpFixture]
    public class SetupFixture1
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IOrphanDbService, OrphanDbService>();
            container.RegisterType<IFatherDbService, FatherDbService>();
            container.RegisterType<IFamilyDbService, FamilyDbService>();
            container.RegisterType<IMotherDbService, MotherDbService>();
            container.RegisterType<ICaregiverDbService, CaregiverDbService>();
            container.RegisterType<IGuarantorDbService, GuarantorDbService>();
            container.RegisterType<IAccountDbService, AccountDbService>();
            container.RegisterType<IUserDbService, UserDbService>();
            container.RegisterType<ISelfLoopBlocking, SelfLoopBlocking>();
            container.RegisterType<IAutoCompleteDbService, AutoCompleteDbService>();
            container.RegisterType<IBailDbService, BailDbService>();
            container.RegisterType<IUriGenerator, UriGenerator>();
            container.RegisterType<IHttpMessageConfiguerer, HttpMessageConfiguerer>();
            container.RegisterType<IRegularDataService, RegularDataService>();
            container.RegisterType<IStringsFixer, StringsFixer>();
            container.RegisterType<ICheckerService, CheckerService>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RegisterTypes(container.Value);
            ConfigureMapper();
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // TODO: Add code here that is run after
            //  all tests in the assembly have been run
        }
    }
}