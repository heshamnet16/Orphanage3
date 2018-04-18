using OrphanageService.Services;
using System;
using System.ServiceProcess;
using W.Firewall;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = new Logger();
            try
            {
                CreateFirewallRules();
                if (!PowerShellExecuter.IsExist())
                    PowerShellExecuter.CreateAndRunPsFileScript();

                // commit to debug the service
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

        private static void CreateFirewallRules()
        {
            var logger = new Logger();
            try
            {
                logger.Information("checking if firewall is existed");
                if (!Rules.Exists("Orphanage3"))
                {
                    logger.Information("trying create firewall rules");
                    Rules.Add("Orphanage3", "", localPorts: "1515");
                    logger.Information("firewall rules are successfully created");
                }
                else
                {
                    logger.Information("firewall rules are already existed");
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("Connot create Orphanage3 rule");
                logger.Fatal(ex.Message);
                if (ex.InnerException != null)
                {
                    logger.Fatal(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Fatal(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {
                            logger.Fatal(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
            }
        }
    }
}