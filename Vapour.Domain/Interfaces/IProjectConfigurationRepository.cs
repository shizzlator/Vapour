namespace Vapour.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration GetConfig(string projectName, string environment, string testDescription);
        void Save(ProjectConfiguration projectConfiguration);
    }
}