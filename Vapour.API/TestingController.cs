using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.Interfaces;
using TestOutput = Vapour.Domain.TestOutput;

namespace Vapour.API
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly ITestOutputFactory _testOutputFactory;

        public TestingController(ITestRunner testRunner, ITestOutputFactory testOutputFactory)
        {
            _testRunner = testRunner;
            _testOutputFactory = testOutputFactory;
        }

        public TestingController() : this(new NunitTestRunner(), new TestOutputFactory())
        {
        }

        [Route("Test/{projectName}/{environment}/{testDescription}")]
        public TestOutput Get(string projectName, string environment, string testDescription)
        {
            var projectConfiguration = new ProjectConfiguration {ProjectName = projectName,Environment = environment,TestDescription = testDescription};

            var testResult = _testRunner.RunTests(projectConfiguration);

            return _testOutputFactory.Create(testResult);
        }

        
    }
}