using System.Configuration;

namespace Vapour.Domain
{
    public class Config : IConfig
    {
        public string DatabaseName { get { return ConfigurationManager.AppSettings["DatabaseName"]; } }
        public string ConnectionString { get { return ConfigurationManager.AppSettings["ConnectionString"]; } }
        public string AssemblyStorePath { get { return ConfigurationManager.AppSettings["AssemblyStorePath"]; } }
    }
}