using System;
using NUnit.Core;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Domain.TestRunner
{
    public class NUnitTestRunner : ITestRunner
    {
        private readonly IAssemblyConfigWriter _assemblyConfigWriter;
        private readonly IConfig _config;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public NUnitTestRunner(IAssemblyConfigWriter assemblyConfigWriter, IConfig config, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _assemblyConfigWriter = assemblyConfigWriter;
            _config = config;
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public NUnitTestRunner() : this(new AssemblyConfigWriter(), new Config(), new ProjectConfigurationRepository())
        {
        }

        public TestResult RunTests(ProjectConfiguration projectConfiguration)
        {
			// Guards
			string projectName = projectConfiguration.ProjectName;
			if (string.IsNullOrEmpty(projectName))
				throw new ArgumentNullException("projectConfiguration", "The projectConfiguration contained an empty id");

			string description = projectConfiguration.TestDescription;
			if (string.IsNullOrEmpty(projectName))
				throw new ArgumentNullException("projectConfiguration", "The projectConfiguration contained an empty description");

			string environment = projectConfiguration.Environment;
			if (string.IsNullOrEmpty(projectName))
				throw new ArgumentNullException("projectConfiguration", "The projectConfiguration contained an empty environment");
			
            CoreExtensions.Host.InitializeService();
	        
			// Look up the full object from MongoDB
            projectConfiguration = _projectConfigurationRepository.Get(projectConfiguration);
	        if (projectConfiguration == null)
	        {
		        throw new VapourException(
			        string.Format("MongoDB returned a null projectConfiguration for: project name:{0}, description:{1}, environment: {2} ",
									projectName, description, environment));
	        }

	        // Write the .config file
			if (projectConfiguration.ConfigurationCollection != null && projectConfiguration.ConfigurationCollection.Count > 0)
				_assemblyConfigWriter.WriteConfigFor(projectConfiguration);

            string pathToAssembly = projectConfiguration.GetAssemblyPathFor(_config.AssemblyStorePath);

            var remoteTestRunner = new RemoteTestRunner();
            var testPackage = new TestPackage(pathToAssembly);
            remoteTestRunner.Load(testPackage);

            TestResult testResult = remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);

            CoreExtensions.Host.UnloadService();
            return testResult;
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