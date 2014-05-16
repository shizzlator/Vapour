using System.Collections.Generic;

namespace Vapour.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration Get(ProjectConfiguration projectConfiguration);
        ProjectConfiguration Get(string id);
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
        List<ProjectConfiguration> GetAll();
    }
}