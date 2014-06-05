using System.Web;
using System.Web.Http;
using Vapour.API.Helpers;
using Vapour.API.Models;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.API.Controllers
{
    public class TeamCityArtifactController : ApiController
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public TeamCityArtifactController(IProjectConfigurationRepository projectConfigurationRepository, IConfig config)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _config = config;
        }

        public TeamCityArtifactController()
            : this(new ProjectConfigurationRepository(), new Config())
        {
        }

        [Route("TeamCityArtifactDownload/{projectName}/{environment}/{testDescription}")]
        public ArtifactDownloadOutputModel Get([FromUri]ProjectConfiguration projectConfiguration)
        {
            var queryString = HttpContext.Current.Request.QueryString;
            var artifactsUrl = queryString["artifactUrl"];

            if (artifactsUrl != null)
            {
                var tcAssemblyDownloader = new TeamCityAssemblyDownloader(_config, _projectConfigurationRepository);
                var result = tcAssemblyDownloader.DownloadAssembly(projectConfiguration, artifactsUrl);

                return new ArtifactDownloadOutputModel { Success = result };
            }

            return new ArtifactDownloadOutputModel
            {
                Success = false,
                ErrorMessage = "'artifactUrl' was not provided in query string"
            };
        }
    }
}
