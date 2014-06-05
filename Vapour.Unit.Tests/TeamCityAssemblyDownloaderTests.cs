using System.IO;
using Moq;
using NUnit.Framework;
using Vapour.API;
using Vapour.API.Helpers;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class TeamCityAssemblyDownloaderTests
    {
        private Mock<IConfig> _fakeConfig;
        private Mock<IProjectConfigurationRepository> _fakeProjConfRepo;
        private readonly string downloadPath = Path.GetTempPath() + "VapourPckgs";

        private readonly ProjectConfiguration _testFullProjectConfiguration = new ProjectConfiguration
        {
            AssemblyName = "Project.Tests.Smoke",
            Environment = "PAT",
            ProjectName = "Project",
            TestDescription = "ProjectDesc"
        };

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists(downloadPath))
                Directory.Delete(downloadPath, true);

            Directory.CreateDirectory(downloadPath);

            _fakeConfig = new Mock<IConfig>();
            _fakeConfig.Setup(x => x.AssemblyStorePath).Returns(downloadPath);

            _fakeProjConfRepo = new Mock<IProjectConfigurationRepository>();
            _fakeProjConfRepo.Setup(x => x.Get(It.IsAny<ProjectConfiguration>())).Returns(_testFullProjectConfiguration);
        }


        [Test]
        public void should_nupkg_be_downloaded_correctly()
        {
            //given
            var assemblyDownloader = new TeamCityAssemblyDownloader(_fakeConfig.Object, _fakeProjConfRepo.Object);
            string localFilePath = Directory.GetCurrentDirectory() + "/TestContent/VAPOUR_PROJ_NAME.1.0.nupkg";
            var projConfiguration = new ProjectConfiguration
            {
                ProjectName = "testProjectName",
                TestDescription = "testDescription",
                Environment = "testEnvironment"
            };

            //when
            var result = assemblyDownloader.DownloadAssembly(projConfiguration, localFilePath);

            //then
            Assert.That(result, Is.EqualTo(true));
        }
    }
}