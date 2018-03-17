using OrphanageV3.Controlls;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.Views.Helper;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Registration;

namespace OrphanageV3
{
    static class Program
    {
        public static IUnityContainer Factory;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Factory = BuildContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.Orphan.OrphansView());
        }

        public static IUnityContainer BuildContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IApiClient, ApiClient>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new object[] { true}));
            currentContainer.RegisterSingleton<ITranslateService, TranslateService>();
            currentContainer.RegisterSingleton<IMapperService, MapperService>();
            currentContainer.RegisterType<IRadGridHelper, RadGridHelper>();
            currentContainer.RegisterSingleton<IDataFormatterService, DataFormatterService>();
            currentContainer.RegisterSingleton<IAutoCompleteService, AutoCompleteService>();
            currentContainer.RegisterType<IEntityValidator, EntityValidator>();            
            currentContainer.RegisterType<OrphansViewModel>();
            currentContainer.RegisterType<CaregiversViewModel>();
            return currentContainer;
        }
    }
}