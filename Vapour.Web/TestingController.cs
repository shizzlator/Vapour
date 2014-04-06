using System.Web.Http;
using Vapour.Domain;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Interfaces;

namespace Vapour.Web
{
    public class TestingController : ApiController
    {
        private readonly ITestRunner _testRunner;
        private readonly IAssemblyConfigWriter _assemblyConfigWriter;

        public TestingController(ITestRunner testRunner, IAssemblyConfigWriter assemblyConfigWriter)
        {
            _testRunner = testRunner;
            _assemblyConfigWriter = assemblyConfigWriter;
        }

        public TestingController() : this(new NunitTestRunner(), new AssemblyConfigWriter())
        {
            //TODO: IoC?
        }

        [Route("{testDescription}/{projectName}/{environment}")]
        public TestOutput Get(string testDescription, string projectName, string environment)
        {
            //TODO: copy dll to run path
            //TODO: save config in run path
            //TODO: run tests
            //TODO: Deal with result!
            //TODO: Chill out

            _assemblyConfigWriter.WriteConfigFor(projectName, environment, testDescription);

            return new TestOutput();
        }
    }
}