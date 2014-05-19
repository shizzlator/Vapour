using Vapour.Domain.DataAccess;

namespace Vapour.Domain.TestRunner
{
    public interface IAssemblyConfigWriter
    {
        void WriteConfigFor(ProjectConfiguration projectConfiguration);
    }
}