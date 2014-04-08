using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
{
    public class AssemblyUploadController : ApiController
    {
        private readonly IConfig _config;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public AssemblyUploadController(IConfig config, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _config = config;
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public AssemblyUploadController() : this(new Config(), new ProjectConfigurationRepository())
        {
        }

        [Route("save/assembly/{projectName}/{environment}/{testDescription}")]
        public async Task<HttpResponseMessage> PostAssembly(string projectName, string environment, string testDescription)
        {
            CheckRequestIsMultipartFormData();

            var projectConfiguration = _projectConfigurationRepository.GetConfig(projectName, environment, testDescription);

            var provider = new MultipartFormDataStreamProvider(GetAssemblyPathFor(projectConfiguration));

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private void CheckRequestIsMultipartFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
        }

        private string GetAssemblyPathFor(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}\\{1}\\{2}\\", _config.AssemblyStorePath.TrimEnd("\\".ToCharArray()),
                projectConfiguration.ProjectName, projectConfiguration.TestDescription);
        }
    }
}