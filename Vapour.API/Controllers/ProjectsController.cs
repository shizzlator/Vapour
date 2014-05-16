using System.Collections.Generic;
using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.API
{
    public class ProjectsController : ApiController
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public ProjectsController(IProjectConfigurationRepository projectConfigurationRepository)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public ProjectsController() : this(new ProjectConfigurationRepository())
        {
            
        }

        [Route("projects/all")]
        public IEnumerable<ProjectConfiguration> GetAll()
        {
            return _projectConfigurationRepository.GetAll();
        }

        [Route("projects/save")]
        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            return _projectConfigurationRepository.Save(projectConfiguration);
        }

        //TODO: Put routes into object and share with service
        [Route("project/{projectName}/{environment}/{testDescription}")]
        public ProjectConfiguration Get([FromUri]ProjectConfiguration projectConfiguration)
        {
            return _projectConfigurationRepository.Get(projectConfiguration);
        }

        //TODO: Put routes into object and share with service
        [Route("project/{id}")]
        public ProjectConfiguration Get([FromUri]string id)
        {
            return _projectConfigurationRepository.Get(id);
        }
    }
}