﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using W.Firewall;

namespace ServiceConfigurer.Services
{
    public class InstallerService
    {
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


        private static string RunPsScript(string psScript)
        {

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            runspace.Open();

            // create a pipeline and feed it the script text

            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(psScript);

            pipeline.Commands.Add("Out-String");

            // execute the script

            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace

            runspace.Close();

            // convert the script result into a single string

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString();
        }

        private static void CreateFirewallRules()
        {
            if (!Rules.Exists("Orphanage3"))
            {
                Rules.Add("Orphanage3", "", localPorts: "1515");
            }
        }
    }
}
