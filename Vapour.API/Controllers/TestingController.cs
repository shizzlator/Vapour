using System.Web;
using System.Web.Http;
using Vapour.API.Models;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;

namespace Vapour.API.Controllers
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly ITestOutputModelFactory _testOutputModelFactory;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfig _config;

        public TestingController(ITestRunner testRunner, ITestOutputModelFactory testOutputModelFactory, IProjectConfigurationRepository _projectConfigurationRepository, IConfig config)
        {
            _testRunner = testRunner;
            _testOutputModelFactory = testOutputModelFactory;
            this._projectConfigurationRepository = _projectConfigurationRepository;
            _config = config;
        }

        public TestingController() : this(new NunitTestRunner(), new TestOutputModelFactory(), new ProjectConfigurationRepository(), new Config())
        {
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public TestOutputModel Get([FromUri]ProjectConfiguration projectConfiguration)
        {
            var queryString = HttpContext.Current.Request.QueryString;
            var artifactsUrl = queryString["artifactUrl"];

            var tcAssemblyDownloader = new TeamCityAssemblyDownloader(_config, _projectConfigurationRepository);
            tcAssemblyDownloader.DownloadAssembly(projectConfiguration, artifactsUrl);

            var testResult = _testRunner.RunTests(projectConfiguration);

            return _testOutputModelFactory.Create(testResult);
        }
    }
}