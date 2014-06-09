using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Domain.TestRunner
{
    public class AssemblyConfigWriter : IAssemblyConfigWriter
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public AssemblyConfigWriter() : this(new ProjectConfigurationRepository(), new Config())
        {
        }

		internal AssemblyConfigWriter(IProjectConfigurationRepository projectConfigurationRepository, IConfig config)
		{
			_projectConfigurationRepository = projectConfigurationRepository;
			_config = config;
		}

        public void WriteConfigFor(ProjectConfiguration projectConfiguration)
        {
            projectConfiguration = _projectConfigurationRepository.Get(projectConfiguration);
            string path = projectConfiguration.GetAssemblyConfigPathFor(_config.AssemblyStorePath);

            WriteConfig(path, projectConfiguration.ConfigurationCollection);
        }

        private void WriteConfig(string path, IDictionary<string, string> newAppSettings)
        {
			XDocument document = XDocument.Load(path);
	        IEnumerable<XElement> appSettings = document.Root.Elements().Where(x => x.Name.LocalName == "appSettings");

	        foreach (XElement element in appSettings.Descendants())
	        {
		        if (element.Name == "add")
		        {
			        string key = element.Attribute("key").Value;
			        if (newAppSettings.ContainsKey(key))
			        {
				        element.Attribute("value").Value = newAppSettings[key];
			        }
		        }
	        }

	        document.Save(path);
        }
    }
}