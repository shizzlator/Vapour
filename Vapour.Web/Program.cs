﻿using System;
using Microsoft.Owin.Hosting;

namespace Vapour.Web
{
    public class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopping...");
            }
        }
    }
}
