namespace Vapour.Domain
{
    public interface IConfig
    {
        string DatabaseName { get; }
        string ConnectionString { get; }
        string AssemblyStorePath { get; }
    }
}