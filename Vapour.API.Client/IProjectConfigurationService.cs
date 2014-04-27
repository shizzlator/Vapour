using System.Collections.Generic;
using Vapour.Domain;

namespace Vapour.API.Client
{
    public interface IProjectConfigurationService
    {
        List<ProjectConfiguration> GetAll();
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
    }
}