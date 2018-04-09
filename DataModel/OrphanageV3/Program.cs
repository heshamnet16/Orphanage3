using OrphanageDataModel.Persons;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Bail;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.ViewModel.Guarantor;
using OrphanageV3.ViewModel.Main;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.Views.Helper;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace OrphanageV3
{
    internal static class Program
    {
        public static IUnityContainer Factory;
        public static User CurrentUser;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Factory = BuildContainer();
                try
                {
                    //Todo set login User
                    var _apiClient = Factory.Resolve<IApiClient>();
                    CurrentUser = _apiClient.UsersController_GetUserAsync(1).Result;
                }
                catch
                {
                    CurrentUser = new User()
                    {
                        IsAdmin = true,
                        UserName = "مدير",
                        Id = 1
                    };
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Views.Main.MainView());
            }
            catch (ApiClientException)
            {
                //Nothing Changed
                //if (apiEx.StatusCode == "304")
                //if (apiException.StatusCode != "404")
                //{
                //    //TODO show error message
                //}
            }
            catch (Exception ex)
            {
            }
        }

        public static IUnityContainer BuildContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IApiClient, ApiClient>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new object[] { true }));
            currentContainer.RegisterSingleton<ITranslateService, TranslateService>();
            currentContainer.RegisterSingleton<IMapperService, MapperService>();
            currentContainer.RegisterType<IRadGridHelper, RadGridHelper>();
            currentContainer.RegisterSingleton<IDataFormatterService, DataFormatterService>();
            currentContainer.RegisterSingleton<IAutoCompleteService, AutoCompleteService>();
            currentContainer.RegisterType<IEntityValidator, EntityValidator>();
            currentContainer.RegisterType<OrphansViewModel>();
            currentContainer.RegisterType<CaregiversViewModel>();
            currentContainer.RegisterType<MothersViewModel>();
            currentContainer.RegisterType<FathersViewModel>();
            currentContainer.RegisterType<FamiliesViewModel>();
            currentContainer.RegisterType<AddOrphanViewModel>();
            currentContainer.RegisterType<MainViewModel>();
            currentContainer.RegisterType<GuarantorsViewModel>();
            currentContainer.RegisterType<BailsViewModel>();
            return currentContainer;
        }
    }
}