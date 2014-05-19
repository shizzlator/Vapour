using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Domain.TestRunner
{
    public interface IAssemblyConfigWriter
    {
        void WriteConfigFor(ProjectConfiguration projectConfiguration);
    }
}