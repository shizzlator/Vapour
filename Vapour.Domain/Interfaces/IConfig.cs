namespace Vapour.Domain.Interfaces
{
    public interface IConfig
    {
        string DatabaseName { get; }
        string ConnectionString { get; }
        string AssemblyStorePath { get; }
    }
}