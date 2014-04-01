using System.Configuration;
using System.Web.Http;
using NUnit.Core;
using Vapour.Domain;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public TestingController(ITestRunner testRunner, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _testRunner = testRunner;
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public TestingController() : this(new NunitTestRunner(), new ProjectConfigurationRepository(new DatabaseSession()))
        {
            //TODO: IoC
        }

        [Route("smoketest/{appName}/{environment}")]
        public TestOutput Get(string appName, string environment)
        {
            //TODO: Get path to DLL
            //TODO: Form test.dll.config file from projectConfiguration document saved in mongoDb
            //TODO: Chill out

            var result = _testRunner.RunTests("FULL PATH TO SOME DLL");
            return new TestOutput() { TestResult = result };
        }
    }

    public class TestOutput
    {
        public TestResult TestResult { get; set; }
    }
}