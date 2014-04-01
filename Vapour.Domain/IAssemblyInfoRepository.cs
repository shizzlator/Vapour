namespace Vapour.Domain
{
    public interface IAssemblyInfoRepository
    {
        string SavePath(string appName, AssemblyInfo assemblyInfo);
    }
}