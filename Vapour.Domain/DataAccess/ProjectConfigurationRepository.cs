using System.Collections.Generic;
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

        public ProjectConfiguration GetConfig(ProjectConfiguration projectConfiguration)
        {
            var result = _databaseSession.RunQuery<ProjectConfiguration>(projectConfiguration, VapourCollections.ProjectConfigurations);

            return result.FirstOrDefault();
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            _databaseSession.Insert<ProjectConfiguration>(projectConfiguration, VapourCollections.ProjectConfigurations);

            return projectConfiguration;
        }

        public List<ProjectConfiguration> GetAll()
        {
            return _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations)
                .FindAllAs<ProjectConfiguration>().ToList();
        }
    }
}