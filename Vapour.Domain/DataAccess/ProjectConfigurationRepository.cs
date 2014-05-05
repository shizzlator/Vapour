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
            var queryObject = new {ProjectName = projectConfiguration.ProjectName, Environment = projectConfiguration.Environment, TestDescription = projectConfiguration.TestDescription};
            var result = _databaseSession.RunQuery<ProjectConfiguration>(queryObject, VapourCollections.ProjectConfigurations);

            return result.FirstOrDefault();
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            _databaseSession.Save<ProjectConfiguration>(projectConfiguration, VapourCollections.ProjectConfigurations);

            return projectConfiguration;
        }

        public List<ProjectConfiguration> GetAll()
        {
            return _databaseSession.GetCollection<ProjectConfiguration>(VapourCollections.ProjectConfigurations)
                .FindAllAs<ProjectConfiguration>().ToList();
        }
    }
}