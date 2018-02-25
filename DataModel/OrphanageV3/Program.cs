using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

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
            Application.Run(new Views.Summary.SummaryView());
        }

        public static IUnityContainer BuildContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterSingleton<IApiClient, ApiClient>();
            currentContainer.RegisterSingleton<ITranslateService,TranslateService>();
            return currentContainer;
        }
    }
}