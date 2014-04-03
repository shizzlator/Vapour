namespace Vapour.Domain
{
    public interface IConfigWriter
    {
        void WriteConfigFor(string appNamestring, string environment, string testDescription);
    }
}