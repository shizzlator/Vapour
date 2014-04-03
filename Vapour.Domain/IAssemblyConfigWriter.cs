namespace Vapour.Domain
{
    public interface IAssemblyConfigWriter
    {
        void WriteConfigFor(string appNamestring, string environment, string testDescription);
    }
}