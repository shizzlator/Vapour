using Vapour.Domain.Models;

namespace Vapour.API
{
    public interface IAssemblyDownloader
    {
        bool DownloadAssembly(ProjectConfiguration partialProjConfiguration, string artifactsUrl);
    }
}