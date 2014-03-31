using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Vapour.Domain
{
    public interface IProjectConfigurationRepository
    {
        ProjectConfiguration GetConfig(string applicationName, string environment);
    }
}