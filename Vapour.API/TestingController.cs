using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.Interfaces;

namespace Vapour.API
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

        public TestingController() : this(new NunitTestRunner(), new TestOutputModelFactory())
        {
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public TestOutputModel Get(string projectName, string environment, string testDescription)
        {
            var projectConfiguration = new ProjectConfiguration {ProjectName = projectName,Environment = environment,TestDescription = testDescription};

            var testResult = _testRunner.RunTests(projectConfiguration);

            return _testOutputModelFactory.Create(testResult);
        }
    }
}