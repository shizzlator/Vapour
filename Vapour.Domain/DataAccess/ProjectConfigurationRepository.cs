using System.Linq;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain.DataAccess
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
            var jsonObject = new {Environment = environment, ProjectName = projectName};

            var result = _databaseSession.RunQuery<ProjectConfiguration>(jsonObject, VapourCollections.ProjectConfigurations);

            return result.FirstOrDefault();
        }

        public ProjectConfiguration Insert(ProjectConfiguration projectConfiguration)
        {
            _databaseSession.Insert<ProjectConfiguration>(projectConfiguration, VapourCollections.ProjectConfigurations);

            return projectConfiguration;
        }
    }
}