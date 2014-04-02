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
        private readonly IAssemblyDetailsRepository _assemblyDetailsRepository;
        private readonly ITestConfigWriter _testConfigWriter;

        public TestingController(ITestRunner testRunner, IProjectConfigurationRepository projectConfigurationRepository, IAssemblyDetailsRepository assemblyDetailsRepository, ITestConfigWriter testConfigWriter)
        {
            _testRunner = testRunner;
            _projectConfigurationRepository = projectConfigurationRepository;
            _assemblyDetailsRepository = assemblyDetailsRepository;
            _testConfigWriter = testConfigWriter;
        }

        public TestingController() : this(new NunitTestRunner(), new ProjectConfigurationRepository(new DatabaseSession()), new AssemblyDetailsRepository(), new TestConfigWriter())
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
            var result = _testRunner.RunTests(_assemblyDetailsRepository.GetPathFor(appName));
            return new TestOutput() { TestResult = result };
        }
    }
}