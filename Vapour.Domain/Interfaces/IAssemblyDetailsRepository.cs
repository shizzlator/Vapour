namespace Vapour.Domain.Interfaces
{
    public interface IAssemblyDetailsRepository
    {
        AssemblyDetails Save(AssemblyDetails assemblyDetails);
        string GetPathFor(string appName);
    }
}