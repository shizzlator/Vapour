using System.Web.Mvc;

namespace Vapour.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsViewModelFactory _projectsViewModelFactory;

        public ProjectsController(IProjectsViewModelFactory projectsViewModelFactory)
        {
            _projectsViewModelFactory = projectsViewModelFactory;
        }

        public ProjectsController() : this(new ProjectsViewModelFactory())
        {
            
        }

        public ActionResult Home()
        {
            return View(_projectsViewModelFactory.Create());
        }
    }
}