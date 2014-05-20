using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Core;
using NUnit.Framework;
using Vapour.API.Models;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class TestObjectFactoryTests
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

            var assemblyWriter = new AssemblyConfigWriter(new StreamWriterWrapper(), new ProjectConfigurationRepository(), _config.Object);
            _nunitTestRunner = new NunitTestRunner(assemblyWriter, _config.Object, _projectConfigurationRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>().RemoveAll();
            File.Delete(_testAssemblyConfigFile);
        }

        [Test]
        public void should_format_test_output_from_nunit()
        {
            //given
            TestResult testResult = _nunitTestRunner.RunTests(_projectConfiguration);

            //when
            var testOutputModel = new TestOutputModelFactory().Create(testResult);

            // then
            Assert.That(testOutputModel.Message, Is.EqualTo("All Tests Passed!"));
            Assert.That(testOutputModel.Success, Is.True);
            Assert.That(testOutputModel.FailedTests, Is.Null);
        }
    }
}