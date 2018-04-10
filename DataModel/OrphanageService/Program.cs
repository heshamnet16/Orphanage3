using AutoMapper;
using Microsoft.Owin.Hosting;
using OrphanageService.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using W.Firewall;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceProcess;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = new Logger();
            try
            {
                logger.Information("Start");
                CreateFirewallRules();
                if (!IsExist())
                    CreateAndRunPsFileScript();

                // commit to debug the service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                        new SelfHostServiceBase()
                };
                ServiceBase.Run(ServicesToRun);

                //uncommit to debug the service
                //string baseUrl = Properties.Settings.Default.BaseURI;
                //_webapp = WebApp.Start<Startup>(baseUrl);
                //Console.WriteLine("Orphan Service is started on port 1515");
                //Console.ReadLine();
            }
            catch (Exception exc)
            {
                logger.Fatal(exc.Message);
            }
        }

        public static void CreateAndRunPsFileScript()
        {
            var logger = new Logger();
            logger.Information("creating the power-shell script file");
            string currentFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string currentPath = System.IO.Path.GetDirectoryName(currentFileName);
            using (System.IO.FileStream fileStream = new System.IO.FileStream("excuteMe.ps1", System.IO.FileMode.Create))
            {
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream))
                {
                    logger.Information("adding the script text to the power-shell file");
                    streamWriter.WriteLine("# import-module \"sqlps\" -DisableNameChecking #...Here it not required.");
                    streamWriter.WriteLine("$ServerName = \".\"");
                    streamWriter.WriteLine($"invoke-sqlcmd -ServerInstance $ServerName -inputFile \"{currentPath}\\alter database cloumns.sql\" | out-File -filepath \"TestOutput.txt\"");
                    //streamWriter.WriteLine($"installutil.exe /u \"{currentFileName}\"");
                    //streamWriter.WriteLine($"installutil.exe \"{currentFileName}\"");
                    streamWriter.WriteLine("SC.exe STOP OrphanageService");
                    streamWriter.WriteLine("SC.exe DELETE OrphanageService");
                    streamWriter.WriteLine($"SC.exe CREATE OrphanageService DisplayName= \"Orphanage Service\" binpath= \"{currentFileName}\" start= auto");
                }
            }
            logger.Information("trying to execute the power-shell script file");
            RunPsScript("excuteMe.ps1");
        }

        private static bool IsExist()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("$service = Get-WmiObject -Class Win32_Service -Filter \"Name = 'OrphanageService'\"");
            stringBuilder.AppendLine("$service.Status");
            string ret = RunPsScript(stringBuilder.ToString());
            if (ret.Contains("OK") || ret.Contains("Degraded"))
                return true;
            else
                return false;
        }

        private static void RunPsScriptFile(string filePath)
        {
            string psScript = string.Empty;
            if (File.Exists(filePath))
                psScript = File.ReadAllText(filePath);
            else
                throw new FileNotFoundException("Wrong path for the script file");
            RunPsScript(psScript);
        }

        private static string RunPsScript(string psScript)
        {
            var logger = new Logger();

            logger.Information("creating RunspaceFactory");
            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            logger.Information("opening the created RunspaceFactory");
            runspace.Open();

            // create a pipeline and feed it the script text

            logger.Information("create a pipeline and feed it the script text");
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(psScript);

            pipeline.Commands.Add("Out-String");

            // execute the script

            logger.Information("try to execute the script");
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace

            logger.Information("closing the RunspaceFactory");
            runspace.Close();

            // convert the script result into a single string

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                logger.Information($"the output: {obj.ToString()}");
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString();
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