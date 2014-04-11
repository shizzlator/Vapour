using System;
using System.Data.Entity.Core.Objects;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vapour.Domain;
using Vapour.Domain.Interfaces;
using Vapour.Web.WebDomain;

namespace Vapour.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsViewModelFactory _projectsViewModelFactory;
        private readonly IConfig _config;
        private readonly IProjectConfigurationService _projectConfigurationService;

        public ProjectsController(IProjectsViewModelFactory projectsViewModelFactory, IConfig config, IProjectConfigurationService projectConfigurationService)
        {
            _projectsViewModelFactory = projectsViewModelFactory;
            _config = config;
            _projectConfigurationService = projectConfigurationService;
        }

        public ProjectsController() : this(new ProjectsViewModelFactory(), new Config(), new ProjectConfigurationService())
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
    }
}