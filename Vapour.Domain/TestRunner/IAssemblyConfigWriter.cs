namespace Vapour.Domain.Interfaces
{
    public interface IAssemblyConfigWriter
    {
        void WriteConfigFor(ProjectConfiguration projectConfiguration);
    }
}