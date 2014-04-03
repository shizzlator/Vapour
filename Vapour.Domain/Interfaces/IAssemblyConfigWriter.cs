namespace Vapour.Domain.Interfaces
{
    public interface IAssemblyConfigWriter
    {
        void WriteConfigFor(string appNamestring, string environment, string testDescription);
    }
}