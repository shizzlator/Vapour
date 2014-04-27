using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class NunitTestRunnerTests
    {
        private DatabaseSession _databaseSession;
        private ProjectConfigurationRepository _projectConfigurationRepository;
        private ProjectConfiguration _projectConfiguration;
        private Dictionary<string, string> _configurationCollection;
        private Mock<IConfig> _config;
        private NunitTestRunner _nunitTestRunner;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _configurationCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            _projectConfiguration = new ProjectConfiguration() { AssemblyName = "FakeProject.Tests", ProjectName = "FakeProject", Environment = "Development", TestDescription = "Smoke", ConfigurationCollection = _configurationCollection };

            _databaseSession = new DatabaseSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
            _projectConfigurationRepository.Save(_projectConfiguration);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations).RemoveAll();
        }

        [SetUp]
        public void SetUp()
        {
            _config = new Mock<IConfig>();
            _nunitTestRunner = new NunitTestRunner(new AssemblyConfigWriter(new StreamWriterWrapper(), new ProjectConfigurationRepository(), _config.Object), _config.Object);

            _config.Setup(x => x.AssemblyStorePath).Returns(AssemblyStorePath());
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(PathToConfig());
        }

        [Test]
        public void ShouldRunTestsForGivenProject()
        {
            var testResult = _nunitTestRunner.RunTests(_projectConfiguration);

            Assert.That(testResult.IsSuccess, Is.True);
        }

        [Test]
        public void ShouldWriteConfigToAssemblyDirectory()
        {
            _nunitTestRunner.RunTests(_projectConfiguration);

            Assert.That(File.Exists(PathToConfig()), Is.True);
        }

        private static string AssemblyStorePath()
        {
            return string.Format("{0}\\..\\..\\TestAssemblies", Directory.GetCurrentDirectory());
        }

        private string PathToConfig()
        {
            return string.Format("{0}\\{1}\\{2}\\{3}.dll.config", AssemblyStorePath(), _projectConfiguration.ProjectName, _projectConfiguration.TestDescription, _projectConfiguration.AssemblyName);
        }
    }
}