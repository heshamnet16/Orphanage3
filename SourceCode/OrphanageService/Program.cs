using OrphanageService.Services.Interfaces;
using System;
using System.ServiceProcess;
using Unity;
using W.Firewall;

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
            AddUser("hesham", logger);
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

        private static void AddUser(string userName, ILogger logger)
        {
            try
            {
                var usr = new OrphanageDataModel.Persons.User()
                {
                    CanAdd = true,
                    CanRead = true,
                    Password = "1234",
                    UserName = userName
                };
                var userDbService = UnityConfig.GetConfiguredContainer().Resolve<IUserDbService>();
                userDbService.AddUser(usr);
            }
            catch { }
        }

        private static void CreateFirewallRules()
        {
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
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