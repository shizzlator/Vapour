using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IConfigWriter _testConfigWriter;

        public TestingController(ITestRunner testRunner, IProjectConfigurationRepository projectConfigurationRepository, IConfigWriter testConfigWriter)
        {
            _testRunner = testRunner;
            _projectConfigurationRepository = projectConfigurationRepository;
            _testConfigWriter = testConfigWriter;
        }

        public TestingController() : this(new NunitTestRunner(), new ProjectConfigurationRepository(), new ConfigWriter())
        {
            //TODO: IoC?
        }

        [Route("{testDescription}/{appName}/{environment}")]
        public TestOutput Get(string appName, string environment, string testDescription)
        {
            //TODO: Get path to DLL
            //TODO: copy dll to run path
            //TODO: Form test.dll.config file from projectConfiguration document saved in mongoDb
            //TODO: save config in run path
            //TODO: run tests
            //TODO: Deal with result!
            //TODO: Chill out

            _testConfigWriter.WriteConfigFor(appName, environment, testDescription);
            var result = _testRunner.RunTests("PATH TO ASSEMBLY");
            return new TestOutput() { TestResult = result };
        }
    }
}