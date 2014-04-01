namespace Vapour.Domain.Interfaces
{
    public interface IAssemblyInfoRepository
    {
        string SavePath(string appName, AssemblyDetails assemblyDetails);
    }
}