using System.Collections.Generic;
using System.Linq;
using Vapour.Domain.Models;

namespace Vapour.Domain.DataAccess
{
    public class ProjectConfigurationRepository : IProjectConfigurationRepository
    {
        private readonly MongoDbSession _databaseSession;

		public ProjectConfigurationRepository(MongoDbSession databaseSession)
        {
            _databaseSession = databaseSession;
        }

        public ProjectConfigurationRepository() : this(new MongoDbSession())
        {
        }

        public ProjectConfiguration Get(ProjectConfiguration projectConfiguration)
        {
            var queryObject = new { ProjectName = projectConfiguration.ProjectName, TestDescription = projectConfiguration.TestDescription, Environment = projectConfiguration.Environment};
            return _databaseSession.Find<ProjectConfiguration>(queryObject).FirstOrDefault();
        }

        public ProjectConfiguration Get(string id)
        {
			return _databaseSession.Find<ProjectConfiguration>().FirstOrDefault(x => x.Id == id);
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            _databaseSession.Save<ProjectConfiguration>(projectConfiguration);
            return projectConfiguration;
        }

        public IEnumerable<ProjectConfiguration> GetAll()
        {
	        return _databaseSession.Find<ProjectConfiguration>().ToList();
        }
    }
}