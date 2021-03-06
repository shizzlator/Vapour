﻿using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Helpers;
using RestSharp;
using Vapour.Domain.Configuration;
using Vapour.Domain.Models;

namespace Vapour.API.Client.Service
{
    public class ProjectConfigurationService : IProjectConfigurationService
    {
        private readonly IConfig _config;

        public ProjectConfigurationService(IConfig config)
        {
            //TODO: wrap up repetive code in Generic Service Wrapper thing, pass into here?
            _config = config;
        }

        public ProjectConfigurationService() : this(new Config())
        {
        }

        public List<ProjectConfiguration> GetAll()
        {
            var client = new RestClient(_config.VapourApiUrl);
            var request = new RestRequest("/projects/all", Method.GET) {RequestFormat = DataFormat.Json};
            
			IRestResponse response = client.Execute(request);
			EnsureStatusOk(response, "GetAll() failed");

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<List<ProjectConfiguration>>(response.Content) : new List<ProjectConfiguration>();
        }

        public ProjectConfiguration Save(ProjectConfiguration projectConfiguration)
        {
            var client = new RestClient(_config.VapourApiUrl);
            var request = new RestRequest("/projects/save", Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(projectConfiguration);

            IRestResponse response = client.Execute(request);
			EnsureStatusOk(response, "Save() failed for {0}", projectConfiguration.Id);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<ProjectConfiguration>(response.Content) : new ProjectConfiguration();
        }

        public ProjectConfiguration Get(ProjectConfiguration projectConfiguration)
        {
            var client = new RestClient(_config.VapourApiUrl);
            string uri = string.Format("/project/{0}/{1}/{2}", projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription);
            var request = new RestRequest(uri, Method.GET) { RequestFormat = DataFormat.Json };

			IRestResponse response = client.Execute(request);
			EnsureStatusOk(response, "Get() failed for id {0}", projectConfiguration.Id);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<ProjectConfiguration>(response.Content) : new ProjectConfiguration();
        }

        public ProjectConfiguration Get(string id)
        {
            var client = new RestClient(_config.VapourApiUrl);
            string uri = string.Format("/project/{0}", id);
            var request = new RestRequest(uri, Method.GET) { RequestFormat = DataFormat.Json };

            IRestResponse response = client.Execute(request);
	        EnsureStatusOk(response, "Get(id) failed for id {0}", id);

            return response.StatusCode == HttpStatusCode.OK ? Json.Decode<ProjectConfiguration>(response.Content) : new ProjectConfiguration();
        }

	    private static void EnsureStatusOk(IRestResponse response, string errorMessage, params string[] args)
	    {
		    if (response.StatusCode != HttpStatusCode.OK)
		    {
			    var errorBuilder = new StringBuilder();
			    errorBuilder.AppendFormat(errorMessage, args);
			    errorBuilder.AppendLine("");
				errorBuilder.AppendLine("Errors:");
				errorBuilder.AppendLine(response.ErrorMessage);
				if (response.ErrorException != null) 
					errorBuilder.AppendLine(response.ErrorException.ToString());
				errorBuilder.AppendLine("HTTP body:");
			    errorBuilder.AppendLine(response.Content);

			    throw new ApiClientException(errorBuilder.ToString());
		    }
	    }
    }
}