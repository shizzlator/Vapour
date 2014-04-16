using System.Web.Mvc;
using Vapour.Domain;
using Vapour.Domain.Interfaces;
using Vapour.Web.WebDomain;

namespace Vapour.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsViewModelFactory _projectsViewModelFactory;
        private readonly IProjectConfigurationService _projectConfigurationService;
        private readonly IConfig _config;

        public ProjectsController(IProjectsViewModelFactory projectsViewModelFactory, IProjectConfigurationService projectConfigurationService, IConfig config)
        {
            _projectsViewModelFactory = projectsViewModelFactory;
            _projectConfigurationService = projectConfigurationService;
            _config = config;
        }

        public ProjectsController() : this(new ProjectsViewModelFactory(), new ProjectConfigurationService(), new Config())
        {
        }

        public ActionResult Home()
        {
            return View(_projectsViewModelFactory.Create());
        }

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(ProjectConfiguration projectConfiguration)
        {
            return Json(_projectConfigurationService.Save(projectConfiguration));
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public ActionResult RunTest(ProjectConfiguration projectConfiguration)
        {
            ViewBag.RunTestApiUrl = CreateApiUrlForTestRun(projectConfiguration);
            return View(projectConfiguration);
        }

        private string CreateApiUrlForTestRun(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}/Test/{1}/{2}/{3}", _config.VapourApiUrl, projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription);
        }
    }
}