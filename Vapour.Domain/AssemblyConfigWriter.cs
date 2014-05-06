﻿using System.Collections.Generic;
using System.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class AssemblyConfigWriter : IAssemblyConfigWriter
    {
        private readonly IStreamWriterWrapper _streamWriter;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _vapourConfig;

        public AssemblyConfigWriter(IStreamWriterWrapper streamWriter, IProjectConfigurationRepository projectConfigurationRepository, IConfig vapourConfig)
        {
            _streamWriter = streamWriter;
            _projectConfigurationRepository = projectConfigurationRepository;
            _vapourConfig = vapourConfig;
        }

        public AssemblyConfigWriter() : this(new StreamWriterWrapper(), new ProjectConfigurationRepository(), new Config())
        {
        }

        public void WriteConfigFor(ProjectConfiguration projectConfiguration)
        {
            projectConfiguration = _projectConfigurationRepository.Get(projectConfiguration);
            var path = GetPathFor(projectConfiguration);
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

        private string GetPathFor(ProjectConfiguration projectConfiguration)
        {
            //TODO: Use Path.Combine
            return string.Format("{0}\\{1}\\{2}\\{3}.dll.config", _vapourConfig.AssemblyStorePath.TrimEnd("\\".ToCharArray()),
                projectConfiguration.ProjectName, projectConfiguration.TestDescription, projectConfiguration.AssemblyName);
        }
    }
}