using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using W.Firewall;

namespace ServiceConfigurer.Services
{
    public class InstallerService
    {
        private string _ServiceName = "OrphanageService";

        public bool IsServiceInstalled()
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

        public void InstallService()
        {
            string currentFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string currentPath = System.IO.Path.GetDirectoryName(currentFileName);
            string serviceFileName = currentPath + "\\OrphanageService.exe";
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine("SC.exe DELETE OrphanageService");
            stringBuilder.AppendLine($"SC.exe CREATE OrphanageService DisplayName= \"Orphanage Service\" binpath= \"{serviceFileName}\" start= auto");
            RunPsScript(stringBuilder.ToString());
            CreateFirewallRules();
        }

        public void UninstallService()
        {
            string currentFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string currentPath = System.IO.Path.GetDirectoryName(currentFileName);
            string serviceFileName = currentPath + "\\OrphanageService.exe";
            StringBuilder stringBuilder = new StringBuilder();
            StopService();
            stringBuilder.AppendLine("SC.exe DELETE OrphanageService");
            RunPsScript(stringBuilder.ToString());
        }
        private string RunPsScript(string psScript)
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

        private void CreateFirewallRules()
        {
            if (!Rules.Exists("Orphanage3"))
            {
                Rules.Add("Orphanage3", "", localPorts: "1515");
            }
        }

        public bool IsRunning()
        {
                using (ServiceController serviceController = new ServiceController(_ServiceName))
                    return serviceController.Status == ServiceControllerStatus.Running;
            
        }

        public void StartService()
        {
            ServiceController serviceController = new ServiceController(_ServiceName);
            serviceController.Start();
            serviceController.Close();

        }

        public void StopService()
        {
            ServiceController serviceController = new ServiceController(_ServiceName);
            if(serviceController.CanStop)
                serviceController.Stop();
            serviceController.Close();
        }

        public string[] LoadCurrentCACertificate()
        {
            using (X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach(var cert in store.Certificates)
                {
                    if(cert.Issuer.Contains("Orphanage3"))
                    {
                        return new string[] { cert.Verify().ToString() ,cert.NotBefore.ToShortDateString(),cert.NotAfter.ToShortDateString() };
                    }
                }
            }
            return null;
        }

        public string[] LoadCurrentLocalhostCertificate()
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (var cert in store.Certificates)
                {
                    if (cert.Issuer.Contains("Orphanage3"))
                    {
                        return new string[] { (cert.NotAfter >= DateTime.Now).ToString(), cert.NotBefore.ToShortDateString(), cert.NotAfter.ToShortDateString() };
                    }
                }
            }
            return null;
        }

        public void InstallCACertificate()
        {
            X509Certificate2 certificate = new X509Certificate2(Properties.Resources.Orphanage3CA_Pfx, "1111", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            using (X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
            }
        }

        public void InstallCertificate(string pfxPath)
        {
            X509Certificate2 certificate = new X509Certificate2(pfxPath, "2222", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
            }
            string installArgs = $"http add sslcert ipport=0.0.0.0:1515 certhash={certificate.Thumbprint} appid=\"{{7fb5e937-fae6-4a43-b108-36c0b1143adb}}\"";
            string deleteArgs = "http delete sslcert ipport=0.0.0.0:1515";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("netsh.exe", installArgs);
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            Process p = Process.Start(processStartInfo);
            string output = p.StandardOutput.ReadToEnd();
            if(output.Contains("183"))
            {
                //Port is already set
                processStartInfo.Arguments = deleteArgs;
                p = Process.Start(processStartInfo);
                output = p.StandardOutput.ReadToEnd();
                processStartInfo.Arguments = installArgs;
                p = Process.Start(processStartInfo);
                output = p.StandardOutput.ReadToEnd();
            }
            p.WaitForExit();

        }

        public string[] getCertificate(string pfxPath)
        {
            X509Certificate2 cert = new X509Certificate2(pfxPath, "2222", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            return new string[] { (cert.NotAfter >= DateTime.Now).ToString(), cert.NotBefore.ToShortDateString(), cert.NotAfter.ToShortDateString() };
        }
    }
}
