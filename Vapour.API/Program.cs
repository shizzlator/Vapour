using System;
using Microsoft.Owin.Hosting;

namespace Vapour.API
{
    public class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8041";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopping...");
            }
        }
    }
}
