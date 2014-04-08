using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
{
    public class ConfigController : ApiController
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public ConfigController(IProjectConfigurationRepository projectConfigurationRepository)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public ConfigController() : this(new ProjectConfigurationRepository())
        {
            
        }

        [Route("config/save/")]
        public ProjectConfiguration Post(ProjectConfiguration projectConfiguration)
        {
            return _projectConfigurationRepository.Save(projectConfiguration);
        }
    }
}