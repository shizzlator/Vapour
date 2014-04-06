using System;
using NUnit.Core;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class NunitTestRunner : ITestRunner
    {
        private readonly IAssemblyConfigWriter _assemblyConfigWriter;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public NunitTestRunner(IAssemblyConfigWriter assemblyConfigWriter, IProjectConfigurationRepository projectConfigurationRepository, IConfig config)
        {
            _assemblyConfigWriter = assemblyConfigWriter;
            _projectConfigurationRepository = projectConfigurationRepository;
            _config = config;
        }

        public NunitTestRunner() : this(new AssemblyConfigWriter(), new ProjectConfigurationRepository(), new Config())
        {
        }

        //TODO: change to just take a ProjectConfiguration
        public TestResult RunTests(string projectName, string environment, string testDescription)
        {
            CoreExtensions.Host.InitializeService();

            _assemblyConfigWriter.WriteConfigFor(projectName, environment, testDescription);
            var pathToAssembly = GetAssemblyPathFor(_projectConfigurationRepository.GetConfig(projectName, environment, testDescription));

            var remoteTestRunner = new RemoteTestRunner();
            remoteTestRunner.Load(new TestPackage(pathToAssembly));


            return remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);
        }

        public TestResult RunTests(string projectName, string environment, string testDescription, string testFixtureName)
        {
            throw new NotImplementedException();
        }

        public TestResult RunTest(string projectName, string environment, string testDescription, string testFixtureName, string testMethod)
        {
            throw new NotImplementedException();
        }

        private string GetAssemblyPathFor(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}\\{1}\\{2}\\{3}.dll", _config.AssemblyStorePath.TrimEnd("\\".ToCharArray()),
                projectConfiguration.ProjectName, projectConfiguration.TestDescription, projectConfiguration.AssemblyName);
        }
    }
}