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
	    private Mock<IAssemblyConfigWriter> _mockAssemblyWriter;
        private NUnitTestRunner _nunitTestRunner;
	    private string _assemblyPath;
        
        [SetUp]
        public void SetUp()
        {
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

	        _assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAssemblies");
            _config = new Mock<IConfig>();
			_config.Setup(x => x.AssemblyStorePath).Returns(_assemblyPath);

			_mockAssemblyWriter = new Mock<IAssemblyConfigWriter>();
			_nunitTestRunner = new NUnitTestRunner(_mockAssemblyWriter.Object, _config.Object, _projectConfigurationRepository);
        }

	    [TearDown]
        public void TearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>().RemoveAll();
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
			// given + when
			_nunitTestRunner.RunTests(_projectConfiguration);

			// then
			_mockAssemblyWriter.Verify(x => x.WriteConfigFor(It.IsAny<ProjectConfiguration>()));
        }
    }
}