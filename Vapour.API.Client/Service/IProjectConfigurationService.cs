using System.Collections.Generic;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.API.Client.Service
{
    public interface IProjectConfigurationService
    {
        List<ProjectConfiguration> GetAll();
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
        ProjectConfiguration Get(ProjectConfiguration projectConfiguration);
        ProjectConfiguration Get(string id);
    }
}