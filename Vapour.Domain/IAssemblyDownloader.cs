using Vapour.Domain.Models;

namespace Vapour.Domain
{
    public interface IAssemblyDownloader
    {
        bool DownloadAssembly(ProjectConfiguration partialProjConfiguration, string artifactsUrl);
    }
}