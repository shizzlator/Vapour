using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.Interfaces;
using Vapour.Web.Interfaces;

namespace Vapour.Web
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IAssemblyPathFinder _assemblyPathFinder;
        private readonly ITestConfigWriter _testConfigWriter;

        public TestingController(ITestRunner testRunner, IProjectConfigurationRepository projectConfigurationRepository, IAssemblyPathFinder assemblyPathFinder, ITestConfigWriter testConfigWriter)
        {
            _testRunner = testRunner;
            _projectConfigurationRepository = projectConfigurationRepository;
            _assemblyPathFinder = assemblyPathFinder;
            _testConfigWriter = testConfigWriter;
        }

        public TestingController(ITestConfigWriter testConfigWriter) : this(new NunitTestRunner(), new ProjectConfigurationRepository(new DatabaseSession()), new AssemblyPathFinder(), testConfigWriter)
        {
            //TODO: IoC?
        }

        [Route("smoketest/{appName}/{environment}")]
        public TestOutput Get(string appName, string environment)
        {
            //TODO: Get path to DLL
            //TODO: Form test.dll.config file from projectConfiguration document saved in mongoDb
            //TODO: Deal with result!
            //TODO: Chill out

            _testConfigWriter.WriteConfigFor(appName);
            var result = _testRunner.RunTests(_assemblyPathFinder.GetPathFor(appName));
            return new TestOutput() { TestResult = result };
        }
    }
}