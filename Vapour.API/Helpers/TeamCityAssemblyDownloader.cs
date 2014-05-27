using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using Vapour.Domain;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.API.Helpers
{
    public class TeamCityAssemblyDownloader : IAssemblyDownloader
    {
        private readonly IConfig _config;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public TeamCityAssemblyDownloader(IConfig config, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _config = config;
            _projectConfigurationRepository = projectConfigurationRepository;
        }


        public bool DownloadAssembly(ProjectConfiguration partialProjConfiguration, string artifactsUrl)
        {
            try
            {
                var projConfig = _projectConfigurationRepository.Get(partialProjConfiguration);

                artifactsUrl = PrepareArtifactDownloadUrl(artifactsUrl, projConfig);

                var tempFileName = DownloadNuPkgToTemp(artifactsUrl);
                var assemblyPath = GetAssemblyPathFor(projConfig);

                if (Directory.Exists(assemblyPath))
                    new DirectoryInfo(assemblyPath).Empty();
                else
                    Directory.CreateDirectory(assemblyPath);

                ExtractNuPkg(tempFileName, assemblyPath);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string PrepareArtifactDownloadUrl(string artifactUrl, ProjectConfiguration projConfiguration)
        {
            //remove trailing /
            if (artifactUrl.StartsWith("/"))
                artifactUrl = artifactUrl.TrimStart("/".ToCharArray());

            return _config.TeamCityUrl + artifactUrl.Replace("VAPOUR_PROJ_NAME", projConfiguration.AssemblyName);
        }

        private void ExtractNuPkg(string nupkgPath, string destPath)
        {
            ZipFile.ExtractToDirectory(nupkgPath, destPath);
        }

        private string DownloadNuPkgToTemp(string artifactUrl)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile(artifactUrl, tempFileName);
            }

            return tempFileName;
        }

        private string GetAssemblyPathFor(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}\\{1}\\{2}\\", _config.AssemblyStorePath.TrimEnd("\\".ToCharArray()),
                projectConfiguration.ProjectName, projectConfiguration.TestDescription);
        }
    }
}