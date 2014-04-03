using System.Collections;
using System.Collections.Generic;
using System.IO;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class ConfigWriter : IConfigWriter
    {
        private readonly IStreamWriterWrapper _streamWriter;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public ConfigWriter(IStreamWriterWrapper streamWriter, IProjectConfigurationRepository projectConfigurationRepository, IConfig config)
        {
            _streamWriter = streamWriter;
            _projectConfigurationRepository = projectConfigurationRepository;
            _config = config;
        }

        public ConfigWriter() : this(new StreamWriterWrapper(), new ProjectConfigurationRepository(), new Config())
        {
            
        }

        public void WriteConfigFor(string appName, string environment, string testDescription)
        {
            WriteConfig(_config.AssemblyStorePath, _projectConfigurationRepository.GetConfig(appName, environment, testDescription).ConfigurationCollection);
        }

        private void WriteConfig(string path, IDictionary<string, string> appSettings)
        {
            foreach (var appSetting in appSettings)
            {
                using (_streamWriter.WriteFileTo(path))
                {
                    _streamWriter.WriteLine(string.Format("<add key=\"{0}\" value=\"{1}\" />", appSetting.Key, appSetting.Value));
                }
            }
        }
    }
}