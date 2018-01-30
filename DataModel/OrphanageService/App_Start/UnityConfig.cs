using OrphanageService.Services;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using System;

using Unity;

namespace OrphanageService
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
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
        #endregion

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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
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
            container.RegisterType<IHttpResponseMessageConfiguerer, HttpResponseMessageConfiguerer>();

        }
    }
}