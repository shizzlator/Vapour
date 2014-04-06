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

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _configurationCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            _projectConfiguration = new ProjectConfiguration() { AssemblyName = "FakeProject.Tests", ProjectName = "FakeProject", Environment = "Development", TestDescription = "Smoke", ConfigurationCollection = _configurationCollection };

            _databaseSession = new DatabaseSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
            _projectConfigurationRepository.Insert(_projectConfiguration);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations).RemoveAll();
        }

        [Test]
        public void ShouldRunTestsForGivenProject()
        {
            var config = new Mock<IConfig>();
            var nunitTestRunner = new NunitTestRunner(new AssemblyConfigWriter(new StreamWriterWrapper(), new ProjectConfigurationRepository(), config.Object), new ProjectConfigurationRepository(), config.Object);

            config.Setup(x => x.AssemblyStorePath).Returns(string.Format("{0}\\..\\..\\TestASsemblies", Directory.GetCurrentDirectory()));

            var testResult = nunitTestRunner.RunTests(_projectConfiguration.ProjectName, _projectConfiguration.Environment, _projectConfiguration.TestDescription);

            Assert.That(testResult.IsSuccess, Is.True);
        }
    }
}