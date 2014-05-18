using System.Collections.Generic;
using Vapour.API.Client;
using Vapour.Domain;
using Vapour.Web.Models;
using System.Linq;

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

		public IEnumerable<ProjectViewModel> Create()
		{
			List<ProjectViewModel> viewModelList = new List<ProjectViewModel>();

			List<ProjectConfiguration> configurations = _projectConfigurationService.GetAll();
			string projectName = "";
			ProjectViewModel container = null;

			foreach (ProjectConfiguration config in configurations.OrderBy(x => x.ProjectName))
			{
				// Add a new container for all project settings
				if (!string.IsNullOrEmpty(config.ProjectName) && projectName != config.ProjectName)
				{
					projectName = config.ProjectName;
					container = new ProjectViewModel();
					container.Name = config.ProjectName;

					viewModelList.Add(container);
				}

				// OK
				ConfigurationViewModel details = new ConfigurationViewModel();
				details.Environment = config.Environment;
				details.Id = config.Id;
				details.TestDescription = config.TestDescription;
				
				container.AddProjectDetails(details);
			}

			return viewModelList;
		}
	}
}