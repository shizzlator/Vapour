namespace Vapour.Domain.Configuration
{
    public interface IConfig
    {
        string DatabaseName { get; }
        string ConnectionString { get; }
        string AssemblyStorePath { get; }
        string VapourApiUrl { get; }
        string TeamCityUrl { get; }
    }
}