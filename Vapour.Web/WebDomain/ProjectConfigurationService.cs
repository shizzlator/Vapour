using System.Collections.Generic;
using System.Net;
using System.Web.Helpers;
using RestSharp;
using Vapour.Domain;
using Vapour.Domain.Interfaces;

namespace Vapour.Web.WebDomain
{
    public class ProjectConfigurationService : IProjectConfigurationService
    {
        private readonly IConfig _config;

        public ProjectConfigurationService(IConfig config)
        {
            _config = config;
        }

        public ProjectConfigurationService() : this(new Config())
        {
        }

        public List<ProjectConfiguration> GetAll()
        {
            var client = new RestClient(_config.VapourApiUrl);
            var request = new RestRequest("/projects/all", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<List<ProjectConfiguration>>(response.Content) : new List<ProjectConfiguration>();
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            var client = new RestClient(_config.VapourApiUrl);
            var request = new RestRequest("projects/save", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(projectConfiguration);

            var response = client.Execute(request);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<ProjectConfiguration>(response.Content) : new ProjectConfiguration();
        }
    }
}