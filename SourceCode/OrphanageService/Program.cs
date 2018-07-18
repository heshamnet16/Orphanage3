using Microsoft.Owin.Hosting;
using OrphanageService.Services.Interfaces;
using System;
using System.ServiceProcess;
using Unity;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string logfileName = AppDomain.CurrentDomain.BaseDirectory + "servicelog.log";
            if (System.IO.File.Exists(logfileName))
            {
                try
                {
                    System.IO.File.Delete(logfileName);
                }
                catch { }
            }
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
            try
            {

                // commit to debug the service
                logger.Information("try to startup the service");
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                        new SelfHostServiceBase()
                };
                ServiceBase.Run(ServicesToRun);

                //uncommit to debug the service
                //string baseUrl = Properties.Settings.Default.BaseURI;
                //WebApp.Start<Startup>(baseUrl);
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("Orphan Service is started on port 1515");
                //Console.ForegroundColor = ConsoleColor.White;
                //Console.ReadLine();
            }
            catch (Exception exc)
            {
                logger.Fatal(exc.Message);
            }
        }

    }
}