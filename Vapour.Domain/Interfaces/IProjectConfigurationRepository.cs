using System.Collections.Generic;

namespace Vapour.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration GetConfig(ProjectConfiguration projectConfiguration);
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
        List<ProjectConfiguration> GetAll();
    }
}