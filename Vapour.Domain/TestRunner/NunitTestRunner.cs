using System;
using NUnit.Core;
using Vapour.Domain.Config;
using Vapour.Domain.DataAccess;

namespace Vapour.Domain.TestRunner
{
    public class NunitTestRunner : ITestRunner
    {
        private readonly IAssemblyConfigWriter _assemblyConfigWriter;
        private readonly IConfig _config;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public NunitTestRunner(IAssemblyConfigWriter assemblyConfigWriter, IConfig config, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _assemblyConfigWriter = assemblyConfigWriter;
            _config = config;
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public NunitTestRunner() : this(new AssemblyConfigWriter(), new Config.Config(), new ProjectConfigurationRepository())
        {
        }

        public TestResult RunTests(ProjectConfiguration projectConfiguration)
        {
            CoreExtensions.Host.InitializeService();

            projectConfiguration = _projectConfigurationRepository.Get(projectConfiguration);

            WriteConfig(projectConfiguration);
            string pathToAssembly = projectConfiguration.GetAssemblyPathFor(_config.AssemblyStorePath);

            var remoteTestRunner = new RemoteTestRunner();
            var testPackage = new TestPackage(pathToAssembly);
            remoteTestRunner.Load(testPackage);

            return remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);
        }

        private void WriteConfig(ProjectConfiguration projectConfiguration)
        {
            if(projectConfiguration.ConfigurationCollection != null && projectConfiguration.ConfigurationCollection.Count > 0)
                _assemblyConfigWriter.WriteConfigFor(projectConfiguration);
        }

        public TestResult RunTests(ProjectConfiguration projectConfiguration, string testFixtureName)
        {
            throw new NotImplementedException();
        }

        public TestResult RunTest(ProjectConfiguration projectConfiguration, string testFixtureName, string testMethod)
        {
            throw new NotImplementedException();
        }
    }
}