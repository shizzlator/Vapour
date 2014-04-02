using System.Collections.Generic;
using NUnit.Framework;
using Vapour.Domain;
using Vapour.Domain.DataAccess;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class ProjectConfigurationRepositoryTests
    {
        private DatabaseSession _databaseSession;
        private ProjectConfigurationRepository _projectConfigurationRepository;
        private ProjectConfiguration _projectConfiguration;
        private Dictionary<string, string> _configurationCollection;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _configurationCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            _projectConfiguration = new ProjectConfiguration() { ProjectName = "TestProject", Environment = "Development", ConfigurationCollection = _configurationCollection };

            _databaseSession = new DatabaseSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations).RemoveAll();
        }

        [Test]
        public void ShouldInsertAndRetrieveProjectConfiguration()
        {
            _projectConfigurationRepository.Insert(_projectConfiguration);

            var retrievedConfig = _projectConfigurationRepository.GetConfig("TestProject", "Development");

            Assert.That(retrievedConfig.ConfigurationCollection["baseUrl"], Is.EqualTo(_configurationCollection["baseUrl"]));
            Assert.That(retrievedConfig.ConfigurationCollection["someothersetting"], Is.EqualTo(_configurationCollection["someothersetting"]));
        }
    }
}