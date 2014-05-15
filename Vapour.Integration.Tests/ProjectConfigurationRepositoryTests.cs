using System.Collections.Generic;
using MongoDB.Bson;
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

        [SetUp]
        public void SetUp()
        {
            _configurationCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            _projectConfiguration = new ProjectConfiguration() { ProjectName = "TestProject", Environment = "Enzo", TestDescription = "Smoke", ConfigurationCollection = _configurationCollection };

            _databaseSession = new DatabaseSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations).RemoveAll();
        }

        [Test]
        public void ShouldInsertAndRetrieveProjectConfiguration()
        {
            _projectConfigurationRepository.Save(_projectConfiguration);

            var retrievedConfig = _projectConfigurationRepository.Get(_projectConfiguration);

            Assert.That(retrievedConfig.ConfigurationCollection["baseUrl"], Is.EqualTo(_configurationCollection["baseUrl"]));
            Assert.That(retrievedConfig.ConfigurationCollection["someothersetting"], Is.EqualTo(_configurationCollection["someothersetting"]));
        }

        [Test]
        public void ShouldGetAll()
        {
            _projectConfigurationRepository.Save(_projectConfiguration);
            _projectConfiguration.Id = string.Empty;
            _projectConfiguration.Environment = "Fire";
            _projectConfigurationRepository.Save(_projectConfiguration);

            var projects = _projectConfigurationRepository.GetAll();

            Assert.That(projects[0].Environment, Is.EqualTo("Enzo"));
            Assert.That(projects[1].Environment, Is.EqualTo("Fire"));
        }
    }
}