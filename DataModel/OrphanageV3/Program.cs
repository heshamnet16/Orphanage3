using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.Views.Helper;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            Application.Run(new Views.Orphan.OrphansView());
        }

        public static IUnityContainer BuildContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterSingleton<IApiClient, ApiClient>();
            currentContainer.RegisterSingleton<ITranslateService,TranslateService>();
            currentContainer.RegisterSingleton<IMapperService, MapperService>();
            currentContainer.RegisterSingleton<IRadGridHelper, RadGridHelper>();
            currentContainer.RegisterSingleton<IDataFormatterService, DataFormatterService>();
            return currentContainer;
        }
    }
}