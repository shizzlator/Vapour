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

        public ProjectConfigurationRepository() : this(new DatabaseSession())
        {
        }

        public ProjectConfiguration GetConfig(string projectName, string environment, string testDescription)
        {
            var jsonObject = new {Environment = environment, ProjectName = projectName};

            var result = _databaseSession.RunQuery<ProjectConfiguration>(jsonObject, VapourCollections.ProjectConfigurations);

            return result.FirstOrDefault();
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            _databaseSession.Insert<ProjectConfiguration>(projectConfiguration, VapourCollections.ProjectConfigurations);

            return projectConfiguration;
        }
    }
}