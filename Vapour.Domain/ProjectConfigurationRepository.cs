using MongoDB.Driver.Linq;
using System.Linq;

namespace Vapour.Domain
{
    public class ProjectConfigurationRepository : IProjectConfigurationRepository
    {
        private readonly IDatabaseSession _databaseSession;

        public ProjectConfigurationRepository(IDatabaseSession databaseSession)
        {
            _databaseSession = databaseSession;
        }

        public ProjectConfiguration GetConfig(string projectName, string environment)
        {
            var colletion = _databaseSession.GetCollection<ProjectConfiguration>("projectconfigurations");

            var query = from config in colletion.AsQueryable<ProjectConfiguration>()
                        where config.Environment == environment && config.ProjectName == projectName
                        select config;

            return query.FirstOrDefault();
        }

        public ProjectConfiguration Insert(ProjectConfiguration projectConfiguration)
        {
            var collection = _databaseSession.GetCollection<ProjectConfiguration>("projectconfigurations");

            collection.Insert(projectConfiguration);

            return projectConfiguration;
        }
    }
}