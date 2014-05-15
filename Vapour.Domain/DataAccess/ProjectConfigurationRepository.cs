using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
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

        public ProjectConfiguration Get(ProjectConfiguration projectConfiguration)
        {
            var queryObject = new {projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription};
            var result = _databaseSession.RunQuery<ProjectConfiguration>(queryObject, VapourCollections.ProjectConfigurations);

            return result.FirstOrDefault();
        }

        public ProjectConfiguration Get(string id)
        {
            var queryObject = new {_id = ObjectId.Parse(id)};
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