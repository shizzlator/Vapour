using System;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

namespace Vapour.Web
{
    public class Host
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

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }

    public class NunitTestRunner : ITestRunner
    {
        public void RunTests(string pathToAssembly)
        {
            throw new NotImplementedException();
        }

        public void RunTests(string pathToAssembly, string testFixture)
        {
            throw new NotImplementedException();
        }

        public void RunTest(string pathToAssembly, string testMethod)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITestRunner
    {
        void RunTests(string pathToAssembly);
        void RunTests(string pathToAssembly, string testFixture);
        void RunTest(string pathToAssembly, string testMethod);
    }
}
