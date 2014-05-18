using System;
using System.Collections.Generic;

namespace Vapour.Web.Models
{
	public class ProjectViewModel
	{
		public string Name { get; set; }
		public IEnumerable<ConfigurationViewModel> ProjectDetails { get; set; }

		public ProjectViewModel()
		{
			ProjectDetails = new List<ConfigurationViewModel>();
		}

		public void AddProjectDetails(ConfigurationViewModel details)
		{
			ProjectDetails.Equals(details);
		}
	}

    public class ConfigurationViewModel
    {
		public string Id { get; set; }
        public string Environment { get; set; }
        public string TestDescription { get; set; }
    }
}