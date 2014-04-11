using System.Configuration;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class Config : IConfig
    {
        public string DatabaseName { get { return ConfigurationManager.AppSettings["DatabaseName"]; } }
        public string ConnectionString { get { return ConfigurationManager.AppSettings["ConnectionString"]; } }
        public string AssemblyStorePath { get { return ConfigurationManager.AppSettings["AssemblyStorePath"]; } }
        public string VapourApiUrl { get { return ConfigurationManager.AppSettings["VapourApiUrl"]; } }
        public string TestRunBasePath { get { return ConfigurationManager.AppSettings["TestRunBasePath"]; } }
    }
}