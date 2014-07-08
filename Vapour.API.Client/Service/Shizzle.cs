using System.Collections.Generic;
using System.Net;
using System.Web.Helpers;
using RestSharp;
using Vapour.Domain.Configuration;
using Vapour.Domain.Models;

namespace Vapour.API.Client.Service
{
    public class ProjectConfigurationServiceClient : IProjectConfigurationService
    {
        private readonly IApiClient _apiClient;

        public ProjectConfigurationServiceClient(IConfig config)
        {
            _apiClient = new ApiClient(config.VapourApiUrl);
        }

        public ProjectConfigurationServiceClient()
            : this(new Config())
        {
        }

        public List<ProjectConfiguration> GetAll()
        {
            return _apiClient.Request<List<ProjectConfiguration>>("/projects/all", Method.GET);
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            return _apiClient.Request<ProjectConfiguration>("/projects/save", Method.POST, projectConfiguration);
        }

        public ProjectConfiguration Get(ProjectConfiguration projectConfiguration)
        {
            var uri = string.Format("/project/{0}/{1}/{2}", projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription);

            return _apiClient.Request<ProjectConfiguration>(uri, Method.GET);
        }

        public ProjectConfiguration Get(string id)
        {
            var uri = string.Format("/project/{0}", id);

            return _apiClient.Request<ProjectConfiguration>(uri, Method.GET);
        }
    }

    public interface IApiClient
    {
        T Request<T>(string uri, Method method) where T : new();
        T Request<T>(string uri, Method method, T body) where T : new();
    }

    public class ApiClient : IApiClient
    {
        private readonly RestClient _restClient;

        //TODO: pass in RequestProvider
        public ApiClient(string url)
        {
            _restClient = new RestClient(url);
        }

        public T Request<T>(string uri, Method method) where T : new()
        {
            var request = new RestRequest(uri, method) { RequestFormat = DataFormat.Json };
            var response = _restClient.Execute(request);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<T>(response.Content) : new T();
        }

        public T Request<T>(string uri, Method method, T body) where T : new()
        {
            var request = new RestRequest(uri, method) { RequestFormat = DataFormat.Json };
            request.AddBody(body);

            var response = _restClient.Execute(request);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<T>(response.Content) : new T();
        }
    }
    
}

