using System.Web.Mvc;
using Vapour.API.Client;
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

        public ActionResult Index()
        {
            return View("Home", _projectsViewModelFactory.Create());
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(ProjectConfiguration projectConfiguration)
        {
            projectConfiguration = _projectConfigurationService.Save(projectConfiguration);
            return RedirectToAction("NewConfig", projectConfiguration);
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public ActionResult RunTest(ProjectConfiguration projectConfiguration)
        {
            ViewBag.RunTestApiUrl = CreateApiUrlForTestRun(projectConfiguration);
            return View(projectConfiguration);
        }

        [Route("Edit/{projectName}/{environment}/{testDescription}")]
        public ActionResult Edit(ProjectConfiguration projectConfiguration)
        {
            return View(_projectConfigurationService.Get(projectConfiguration));
        }

        public ActionResult NewConfig(ProjectConfiguration projectConfiguration)
        {
            return View(projectConfiguration);
        }

        [HttpPost]
        public ActionResult SaveConfig(ProjectConfiguration projectConfiguration)
        {
            return RedirectToAction("Index");
        }

        private string CreateApiUrlForTestRun(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}/Test/{1}/{2}/{3}", _config.VapourApiUrl, projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription);
        }
    }
}