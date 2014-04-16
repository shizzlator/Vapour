using System.Collections.Generic;
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

        public ProjectsViewModelFactory() : this(new ProjectConfigurationService())
        {
        }

        public ProjectsViewModel Create()
        {
            var projectsViewModel = new ProjectsViewModel(){Projects = new Dictionary<string, ProjectDetail>()};
            var configurations = _projectConfigurationService.GetAll();

            foreach (var projectConfiguration in configurations)
            {
                if (projectsViewModel.Projects.ContainsKey(projectConfiguration.ProjectName))
                {
                    projectsViewModel.Projects[projectConfiguration.ProjectName].Environments.Add(projectConfiguration.Environment);
                    projectsViewModel.Projects[projectConfiguration.ProjectName].TestDescriptions.Add(projectConfiguration.TestDescription);
                }
                else
                {
                    projectsViewModel.Projects.Add(projectConfiguration.ProjectName, new ProjectDetail()
                    {
                        Environments = new List<string> { projectConfiguration.Environment},
                        TestDescriptions = new HashSet<string> { projectConfiguration.TestDescription },
                    });
                }

            }
            return projectsViewModel;
        }
    }
}