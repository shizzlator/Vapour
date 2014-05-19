using System.Collections.Generic;
using Vapour.Domain.Config;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Domain.TestRunner
{
    public class AssemblyConfigWriter : IAssemblyConfigWriter
    {
        private readonly IStreamWriterWrapper _streamWriter;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public AssemblyConfigWriter(IStreamWriterWrapper streamWriter, IProjectConfigurationRepository projectConfigurationRepository, IConfig config)
        {
            _streamWriter = streamWriter;
            _projectConfigurationRepository = projectConfigurationRepository;
            _config = config;
        }

        public AssemblyConfigWriter() : this(new StreamWriterWrapper(), new ProjectConfigurationRepository(), new Config.Config())
        {
        }

        public void WriteConfigFor(ProjectConfiguration projectConfiguration)
        {
            projectConfiguration = _projectConfigurationRepository.Get(projectConfiguration);
            string path = projectConfiguration.GetAssemblyConfigPathFor(_config.AssemblyStorePath);

            WriteConfig(path, projectConfiguration.ConfigurationCollection);
        }

        private void WriteConfig(string path, IDictionary<string, string> appSettings)
        {
            using (_streamWriter.CreateFile(path))
            {
                _streamWriter.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?><configuration><appSettings>");
                foreach (var appSetting in appSettings)
                {
                    _streamWriter.WriteLine(string.Format(@"<add key=""{0}"" value=""{1}"" />", appSetting.Key, appSetting.Value));
                }
                _streamWriter.WriteLine(@"</appSettings></configuration>");
            }
        }


    }
}