using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Core;
using NUnit.Framework;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class NunitTestRunnerTests
    {
        private MongoDbSession _databaseSession;
        private ProjectConfigurationRepository _projectConfigurationRepository;
        private ProjectConfiguration _projectConfiguration;
        private Dictionary<string, string> _configurationCollection;
        private Mock<IConfig> _config;
        private NunitTestRunner _nunitTestRunner;
	    private string _testAssembliesDirectory;
		private string _testAssemblyConfigFile;

        
        [SetUp]
        public void SetUp()
        {
			// .\ProjectName\Description\FakeProject.Tests.dll is copied as content on the build
	        _testAssembliesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAssemblies");

            _configurationCollection = new Dictionary<string, string>()
            {
	            { "baseUrl", "blah.com" }, 
				{ "someothersetting", "someothervalue" }
            };

            _projectConfiguration = new ProjectConfiguration()
            {
	            AssemblyName = "FakeProject.Tests", 
				ProjectName = "FakeProject", 
				Environment = "Development", 
				TestDescription = "Smoke", 
				ConfigurationCollection = _configurationCollection
            };

            _databaseSession = new MongoDbSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
            _projectConfigurationRepository.Save(_projectConfiguration);

            _config = new Mock<IConfig>();
			_config.Setup(x => x.AssemblyStorePath).Returns(_testAssembliesDirectory);
	        _testAssemblyConfigFile = _projectConfiguration.GetAssemblyConfigPathFor(_testAssembliesDirectory);

	        var assemblyWriter = new AssemblyConfigWriter(new ProjectConfigurationRepository(), _config.Object);
			_nunitTestRunner = new NunitTestRunner(assemblyWriter, _config.Object, _projectConfigurationRepository);
        }

	    [TearDown]
        public void TearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>().RemoveAll();
		    //File.Delete(_testAssemblyConfigFile);
        }

        [Test]
		public void RunTests_should_return_testresult()
        {
			// given + when
			TestResult testResult = _nunitTestRunner.RunTests(_projectConfiguration);

			// then
	        Assert.That(testResult, Is.Not.Null);
            Assert.That(testResult.IsSuccess, Is.True);
        }

        [Test]
		public void RunTests_should_write_appconfig_file()
        {
			// given
			string configPath = _projectConfiguration.GetAssemblyConfigPathFor(_testAssembliesDirectory);

			// when
			_nunitTestRunner.RunTests(_projectConfiguration);

			// then
			Assert.That(File.Exists(configPath), Is.True);
        }

        [Test]
		[Ignore("TODO: Fix")]
		public void RunTests_should_not_write_appconfig_file_when_no_projects_configuration_exists()
        {
			// given
			_databaseSession.GetCollection<ProjectConfiguration>().RemoveAll();
			_projectConfiguration.ConfigurationCollection.Clear();
			_projectConfigurationRepository.Save(_projectConfiguration);
			string configPath = _projectConfiguration.GetAssemblyConfigPathFor(_testAssembliesDirectory);

			// when
			_nunitTestRunner.RunTests(_projectConfiguration);

			// then
			Assert.That(File.Exists(configPath), Is.False);
        }
    }
}