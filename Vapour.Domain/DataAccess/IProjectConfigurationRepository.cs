using System.Collections.Generic;

namespace Vapour.Domain.DataAccess
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration Get(ProjectConfiguration projectConfiguration);
        ProjectConfiguration Get(string id);
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
        IEnumerable<ProjectConfiguration> GetAll();
    }
}