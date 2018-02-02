using Microsoft.Owin.Hosting;
using System;

namespace OrphanageService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string baseUrl = Properties.Settings.Default.BaseURI;

            WebApp.Start<Startup>(baseUrl);

            Console.WriteLine("Orphan Service is started on port 1515");
            Console.ReadLine();
        }
    }
}