using System.Collections.Generic;
using System.Net;
using RestSharp;
using Vapour.Domain;

namespace Vapour.Web.WebDomain
{
    public interface IProjectConfigurationService
    {
        List<ProjectConfiguration> GetAll();
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
    }
}