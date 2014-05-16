using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain.DataAccess
{
    public class ProjectConfigurationRepository : IProjectConfigurationRepository
    {
        private readonly MongoDBSession _databaseSession;

		public ProjectConfigurationRepository(MongoDBSession databaseSession)
        {
            _databaseSession = databaseSession;
        }

        public ProjectConfigurationRepository() : this(new MongoDBSession())
        {
        }

        public ProjectConfiguration Get(ProjectConfiguration projectConfiguration)
        {
	        return _databaseSession.Find<ProjectConfiguration>().FirstOrDefault(x => x.Id == projectConfiguration.Id);
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