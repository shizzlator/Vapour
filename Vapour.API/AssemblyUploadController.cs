using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.API
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

        [Route("new/project/{projectName}/{environment}/{testDescription}")]
        public async Task<HttpResponseMessage> PostAssembly(string projectName, string environment, string testDescription)
        {
            CheckRequestIsMultipartFormData();

            var projectConfiguration = _projectConfigurationRepository.GetConfig(projectName, environment, testDescription);

            string assemblyPath = GetAssemblyPathFor(projectConfiguration);

            var provider = new MultipartFormDataStreamProvider(assemblyPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                FixFileName(provider, assemblyPath);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private static void FixFileName(MultipartFormDataStreamProvider provider, string assemblyPath)
        {
            var assembly = provider.FileData.First();
            File.Move(assembly.LocalFileName, Path.Combine(assemblyPath, assembly.Headers.ContentDisposition.FileName.Trim('"')));
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