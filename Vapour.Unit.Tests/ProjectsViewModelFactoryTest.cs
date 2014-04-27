using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Vapour.API.Client;
using Vapour.Domain;
using Vapour.Web.WebDomain;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class ProjectsViewModelFactoryTest
    {
        [Test]
        public void ShouldPopulateProjectViewModel()
        {
            var fakeProjConfigRepo = new Mock<IProjectConfigurationService>();
            var projectsViewModelFactory = new ProjectsViewModelFactory(fakeProjConfigRepo.Object);

            fakeProjConfigRepo.Setup(x => x.GetAll()).Returns(BuildProjectCollection());

            var projectsViewModel = projectsViewModelFactory.Create();

            Assert.That(projectsViewModel.Projects["fakeproject1"].Environments[0], Is.EqualTo("fire"));
            Assert.That(projectsViewModel.Projects["fakeproject2"].Environments[1], Is.EqualTo("enzo"));
        }

        private List<ProjectConfiguration> BuildProjectCollection()
        {
            return new List<ProjectConfiguration>
            {
                new ProjectConfiguration()
                {
                    ProjectName = "fakeproject1",
                    Environment = "fire"
                },
                new ProjectConfiguration()
                {
                    ProjectName = "fakeproject1",
                    Environment = "enzo"
                },
                new ProjectConfiguration()
                {
                    ProjectName = "fakeproject2",
                    Environment = "fire"
                },
                new ProjectConfiguration()
                {
                    ProjectName = "fakeproject2",
                    Environment = "enzo"
                }
            };
        }
    }
}