using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using NUnit.Framework;
using Vapour.Domain;
using Vapour.Domain.DataAccess;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class ProjectConfigurationRepositoryTests
    {
        private MongoDBSession _databaseSession;
        private ProjectConfigurationRepository _projectConfigurationRepository;
        private ProjectConfiguration _projectConfiguration;
        private Dictionary<string, string> _configurationCollection;

        [SetUp]
        public void SetUp()
        {
            _configurationCollection = new Dictionary<string, string>() { { "baseUrl", "blah.com" }, { "someothersetting", "someothervalue" } };
            _projectConfiguration = new ProjectConfiguration() { ProjectName = "TestProject", Environment = "Enzo", TestDescription = "Smoke", ConfigurationCollection = _configurationCollection };

            _databaseSession = new MongoDBSession();
            _projectConfigurationRepository = new ProjectConfigurationRepository(_databaseSession);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseSession.GetCollection<ProjectConfiguration>().RemoveAll();
        }

        [Test]
        public void should_insert_and_retrieve_project_configuration()
        {
			// given
			_projectConfigurationRepository.Save(_projectConfiguration);

			// when
			ProjectConfiguration retrievedConfig = _projectConfigurationRepository.Get(_projectConfiguration);

			// then
            Assert.That(retrievedConfig.ConfigurationCollection["baseUrl"], Is.EqualTo(_configurationCollection["baseUrl"]));
            Assert.That(retrievedConfig.ConfigurationCollection["someothersetting"], Is.EqualTo(_configurationCollection["someothersetting"]));
        }

        [Test]
		public void GetAll_should_return_all_project_environments()
        {
			// given
			_projectConfigurationRepository.Save(_projectConfiguration);
			_projectConfiguration.Id = string.Empty;
			_projectConfiguration.Environment = "Fire";
			_projectConfigurationRepository.Save(_projectConfiguration);

			// when
			List<ProjectConfiguration> projects = _projectConfigurationRepository.GetAll().ToList();

			// then
            Assert.That(projects[0].Environment, Is.EqualTo("Enzo"));
            Assert.That(projects[1].Environment, Is.EqualTo("Fire"));
        }
    }
}