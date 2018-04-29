using OrphanageService.Services.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Unity;

namespace OrphanageService
{
    public class PowerShellExecuter
    {
        public static void CreateAndRunPsFileScript()
        {
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
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
            RunPsScriptFile("excuteMe.ps1");
        }

        public static bool IsExist()
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
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();

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
    }
}