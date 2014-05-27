using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Vapour.API.Client.Service;
using Vapour.Domain.Models;
using Vapour.Web.WebDomain;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class ProjectsViewModelFactoryTests
    {
        [Test]
        public void Create_should_populate_project_viewmodel()
        {
            // given
            var fakeProjConfigRepo = new Mock<IProjectConfigurationService>();
            var projectsViewModelFactory = new ProjectsViewModelFactory(fakeProjConfigRepo.Object);

            fakeProjConfigRepo.Setup(x => x.GetAll()).Returns(BuildProjectCollection());

            // when
            var projectsViewModel = projectsViewModelFactory.Create();

            // then
            Assert.That(projectsViewModel.Projects["fakeproject1"].Environments[0], Is.EqualTo("fire"));
            Assert.That(projectsViewModel.Projects["fakeproject2"].Environments[1], Is.EqualTo("enzo"));
        }

        private List<ProjectConfiguration> BuildProjectCollection()
        {
            return new List<ProjectConfiguration>
            {
                new ProjectConfiguration
                {
                    ProjectName = "fakeproject1",
                    Environment = "fire"
                },
                new ProjectConfiguration
                {
                    ProjectName = "fakeproject1",
                    Environment = "enzo"
                },
                new ProjectConfiguration
                {
                    ProjectName = "fakeproject2",
                    Environment = "fire"
                },
                new ProjectConfiguration
                {
                    ProjectName = "fakeproject2",
                    Environment = "enzo"
                }
            };
        }
    }
}