using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using NUnit.Framework;
using Vapour.Domain;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class ProjectConfigurationRepositoryTests
    {
        private DatabaseSession _databaseSession;
        private MongoCollection _collection;
        private ProjectConfigurationRepository _projectConfigurationRepository;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _databaseSession = new DatabaseSession();
            _collection = _databaseSession.GetCollection<ProjectConfiguration>("projectconfigurations");
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _collection.RemoveAll();
        }

        [Test]
        public void ShouldInsertProjectConfiguration()
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