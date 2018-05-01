using OrphanageDataModel.Persons;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Bail;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.ViewModel.Guarantor;
using OrphanageV3.ViewModel.Login;
using OrphanageV3.ViewModel.Main;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.ViewModel.Tools;
using OrphanageV3.Views.Helper;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Net.Sockets;
using System.Security.Authentication;
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Views.Main.MainView());
            }
            catch (Exception ex)
            {
                HandleApiExceptions(ex);
            }
        }

        public static void HandleApiExceptions(Exception exception)
        {
            if (exception is ApiClientException)
            {
                var apiEx = (ApiClientException)exception;
                if (apiEx.StatusCode == "304")
                {
                }
                if (apiEx.StatusCode == "401")
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageUnauthorized, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(GetErrorMessage(apiEx), System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (exception is AuthenticationException)
            {
                if (exception.Message.Contains("Token"))
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageNotAuthenticated, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageUnauthorized, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (exception is SocketException)
            {
                var socketException = (SocketException)exception;
                if (socketException.ErrorCode == 10061)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageCannotConnectServer, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (exception.InnerException != null)
                    HandleApiExceptions(exception.InnerException);
            }
        }

        private static string GetErrorMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.Message + Environment.NewLine + GetErrorMessage(ex.InnerException);
            }
            else
            {
                return ex.Message;
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
            currentContainer.RegisterType<IExceptionHandler, ExceptionHandler>();
            currentContainer.RegisterType<OrphansViewModel>();
            currentContainer.RegisterType<CaregiversViewModel>();
            currentContainer.RegisterType<MothersViewModel>();
            currentContainer.RegisterType<FathersViewModel>();
            currentContainer.RegisterType<FamiliesViewModel>();
            currentContainer.RegisterType<AddOrphanViewModel>();
            currentContainer.RegisterSingleton<MainViewModel>();
            currentContainer.RegisterType<GuarantorsViewModel>();
            currentContainer.RegisterType<BailsViewModel>();
            currentContainer.RegisterSingleton<LoginViewModel>();
            currentContainer.RegisterSingleton<DownloadViewModel>();
            return currentContainer;
        }

        public static void RenewApiClient()
        {
            Factory.RegisterType<IApiClient, ApiClient>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new object[] { true }));
            Factory.Resolve<MainViewModel>().UpdateApiClient(Factory.Resolve<IApiClient>());
        }
    }
}