using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
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
            //TODO: IoC?
        }

        [Route("{projectName}/{environment}/{testDescription}")]
        public TestOutput Get(string projectName, string environment, string testDescription)
        {
            //TODO: copy dll to run path
            //TODO: save config in run path
            //TODO: run tests
            //TODO: Deal with result!
            //TODO: Chill out

            var result = _testRunner.RunTests(projectName, environment, testDescription);

            return new TestOutput(){TestResult = result};
        }
    }
}