using System.Collections.Generic;
using System.Linq;
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
        public List<ProjectConfiguration> GetAll()
        {
            return _projectConfigurationRepository.GetAll();
        }

        [Route("projects/save")]
        public ProjectConfiguration Post(ProjectConfiguration projectConfiguration)
        {
            return _projectConfigurationRepository.Save(projectConfiguration);
        }
    }
}