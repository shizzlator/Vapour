using System.Collections.Generic;

namespace Vapour.Web.Models
{
    public class ProjectsViewModel
    {
        public Dictionary<string, ProjectDetail> Projects { get; set; }
    }

    public class ProjectDetail
    {
        public List<string> Environments { get; set; }
        public HashSet<string> TestDescriptions { get; set; }
    }
}