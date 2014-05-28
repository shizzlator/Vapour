using System.Web.Mvc;
using Vapour.API.Client.Service;
using Vapour.Domain.Configuration;
using Vapour.Domain.Models;
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

        public ProjectsController()
            : this(new ProjectsViewModelFactory(), new ProjectConfigurationService(), new Config())
        {
        }

        public ActionResult Index()
        {
            return View("Home", _projectsViewModelFactory.Create());
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new ProjectConfiguration());
        }

        [HttpPost]
        public ActionResult New(ProjectConfiguration projectConfiguration)
        {
            projectConfiguration.HasErrors = false;

            if (!ValidateProjectData(projectConfiguration))
            {
                projectConfiguration.HasErrors = true;
                return View(Trim(projectConfiguration));
            }

            projectConfiguration = _projectConfigurationService.Save(projectConfiguration);
            return RedirectToAction("NewConfig", projectConfiguration);
        }

        [Route("RunTest/{projectName}/{environment}/{testDescription}")]
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
        public ActionResult Save(ProjectConfiguration projectConfiguration)
        {
            _projectConfigurationService.Save(projectConfiguration);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(string id)
        {
            return View(_projectConfigurationService.Get(id));
        }

        private string CreateApiUrlForTestRun(ProjectConfiguration projectConfiguration)
        {
            return string.Format("{0}/Test/{1}/{2}/{3}", _config.VapourApiUrl, projectConfiguration.ProjectName, projectConfiguration.Environment, projectConfiguration.TestDescription);
        }

        private static bool ValidateProjectData(ProjectConfiguration projectConfiguration)
        {
            return !string.IsNullOrEmpty(projectConfiguration.ProjectName) &&
                   !string.IsNullOrEmpty(projectConfiguration.AssemblyName)
                   && !string.IsNullOrEmpty(projectConfiguration.Environment) &&
                   !string.IsNullOrEmpty(projectConfiguration.TestDescription);
        }

        private static ProjectConfiguration Trim(ProjectConfiguration projectConfiguration)
        {
            return new ProjectConfiguration{ProjectName = projectConfiguration.ProjectName.Trim(), 
                AssemblyName = projectConfiguration.AssemblyName.Trim(), 
                Environment = projectConfiguration.Environment.Trim(), 
                TestDescription = projectConfiguration.TestDescription.Trim(), 
                ConfigurationCollection = projectConfiguration.ConfigurationCollection};
        }
    }
}