using System.Web.Http;
using Vapour.API.Models;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;

namespace Vapour.API.Controllers
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly ITestOutputModelFactory _testOutputModelFactory;

        public TestingController(ITestRunner testRunner, ITestOutputModelFactory testOutputModelFactory)
        {
            _testRunner = testRunner;
            _testOutputModelFactory = testOutputModelFactory;
        }

        public TestingController()
            : this(new NunitTestRunner(), new TestOutputModelFactory())
        {
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public TestOutputModel Get([FromUri]ProjectConfiguration projectConfiguration)
        {
            var testResult = _testRunner.RunTests(projectConfiguration);

            return _testOutputModelFactory.Create(testResult);
        }
    }
}