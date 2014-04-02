using System.Collections.Generic;
using MongoDB.Driver;
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

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
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
            var configCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            var projectConfiguration = new ProjectConfiguration(){ProjectName = "TestProject", Environment = "Development", ConfigurationCollection = configCollection};

            _projectConfigurationRepository.Insert(projectConfiguration);

            var retrievedConfig = _projectConfigurationRepository.GetConfig("TestProject", "Development");

            Assert.That(retrievedConfig.ConfigurationCollection["baseUrl"], Is.EqualTo("blah.com"));
            Assert.That(retrievedConfig.ConfigurationCollection["someothersetting"], Is.EqualTo("someothervalue"));
        }
    }
}