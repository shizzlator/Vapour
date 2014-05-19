namespace Vapour.Domain.Config
{
    public interface IConfig
    {
        string DatabaseName { get; }
        string ConnectionString { get; }
        string AssemblyStorePath { get; }
        string VapourApiUrl { get; }
    }
}