using System.Collections.Generic;
using Vapour.API.Client.Service;
using Vapour.Web.Models;

namespace Vapour.Web.WebDomain
{
    public class ProjectsViewModelFactory : IProjectsViewModelFactory
    {
        private readonly IProjectConfigurationService _projectConfigurationService;

        public ProjectsViewModelFactory(IProjectConfigurationService projectConfigurationService)
        {
            _projectConfigurationService = projectConfigurationService;
        }

        public ProjectsViewModelFactory()
            : this(new ProjectConfigurationService())
        {
        }

        public ProjectsViewModel Create()
        {
            var projectsViewModel = new ProjectsViewModel { Projects = new Dictionary<string, ProjectDetail>() };
            var configurations = _projectConfigurationService.GetAll();

            foreach (var projectConfiguration in configurations)
            {
                var projName = projectConfiguration.ProjectName;
                if (projectsViewModel.Projects.ContainsKey(projName))
                {
                    projectsViewModel.Projects[projName].Environments.Add(projectConfiguration.Environment);
                    projectsViewModel.Projects[projName].TestDescriptions.Add(projectConfiguration.TestDescription);
                }
                else
                {
                    projectsViewModel.Projects.Add(projName, new ProjectDetail
                    {
                        Environments = new List<string> { projectConfiguration.Environment },
                        TestDescriptions = new HashSet<string> { projectConfiguration.TestDescription },
                    });
                }

            }
            return projectsViewModel;
        }
    }
}