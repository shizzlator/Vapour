using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.Interfaces;

namespace Vapour.API
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;

        public TestingController(ITestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        public TestingController() : this(new NunitTestRunner())
        {
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public TestOutput Get(ProjectConfiguration projectConfiguration)
        {
            var result = _testRunner.RunTests(projectConfiguration);

            return new TestOutput(){TestResult = result};
        }
    }
}