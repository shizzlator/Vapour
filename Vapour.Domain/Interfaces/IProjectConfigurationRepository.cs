using System.Collections.Generic;

namespace Vapour.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration GetConfig(string projectName, string environment, string testDescription);
        ProjectConfiguration Save(ProjectConfiguration projectConfiguration);
        List<ProjectConfiguration> GetAll();
    }
}