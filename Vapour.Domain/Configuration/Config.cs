using System.Configuration;

namespace Vapour.Domain.Configuration
{
    public class Config : IConfig
    {
        public static Config Current = new Config();
        public string DatabaseName { get { return ConfigurationManager.AppSettings["DatabaseName"]; } }
        public string ConnectionString { get { return ConfigurationManager.AppSettings["ConnectionString"]; } }
        public string AssemblyStorePath { get { return ConfigurationManager.AppSettings["AssemblyStorePath"]; } }
        public string VapourApiUrl { get { return ConfigurationManager.AppSettings["VapourApiUrl"]; } }
        public string TeamCityUrl { get { return ConfigurationManager.AppSettings["TeamCityUrl"]; } }
        public string TestRunBasePath { get { return ConfigurationManager.AppSettings["TestRunBasePath"]; } }
    }
}