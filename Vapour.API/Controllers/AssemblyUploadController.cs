using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vapour.Domain.Configuration;
using Vapour.Domain.Models;

namespace Vapour.API.Controllers
{
    public class AssemblyUploadController : ApiController
    {
        private readonly IConfig _config;

        public AssemblyUploadController(IConfig config)
        {
            _config = config;
        }

        public AssemblyUploadController() : this(new Config())
        {
        }

        [Route("new/project/{projectName}/{environment}/{testDescription}")]
        public async Task<HttpResponseMessage> PostAssembly(ProjectConfiguration projectConfiguration)
        {
            CheckRequestIsMultipartFormData();

            var assemblyPath = GetAssemblyPathFor(projectConfiguration);

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