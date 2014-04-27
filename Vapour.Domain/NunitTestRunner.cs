using System;
using NUnit.Core;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class NunitTestRunner : ITestRunner
    {
        private readonly IAssemblyConfigWriter _assemblyConfigWriter;
        private readonly IConfig _config;

        public NunitTestRunner(IAssemblyConfigWriter assemblyConfigWriter, IConfig config)
        {
            _assemblyConfigWriter = assemblyConfigWriter;
            _config = config;
        }

        public NunitTestRunner() : this(new AssemblyConfigWriter(), new Config())
        {
        }

        //TODO: change to just take a ProjectConfiguration
        public TestResult RunTests(ProjectConfiguration projectConfiguration)
        {
            CoreExtensions.Host.InitializeService();

            _assemblyConfigWriter.WriteConfigFor(projectConfiguration);
            var pathToAssembly = GetAssemblyPathFor(projectConfiguration);

            var remoteTestRunner = new RemoteTestRunner();
            remoteTestRunner.Load(new TestPackage(pathToAssembly));


            return remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);
        }

        public TestResult RunTests(ProjectConfiguration projectConfiguration, string testFixtureName)
        {
            throw new NotImplementedException();
        }

        public TestResult RunTest(ProjectConfiguration projectConfiguration, string testFixtureName, string testMethod)
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