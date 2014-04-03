namespace Vapour.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration GetConfig(string applicationName, string environment, string testDescription);
    }
}