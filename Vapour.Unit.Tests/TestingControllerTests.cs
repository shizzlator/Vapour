using Moq;
using NUnit.Core;
using NUnit.Framework;
using Vapour.Domain.Interfaces;
using Vapour.Web;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class TestingControllerTests
    {
        private const string ASSEMBLY_PATH = "D:\\vapour\\assemblies";
        private const string APP_NAME = "MyProject";

        private Mock<IAssemblyDetailsRepository> _fakeAssemblyDetailsRepository;
        private Mock<IProjectConfigurationRepository> _fakeProjConfigRepository;
        private Mock<ITestRunner> _fakeTestRunner;
        private TestingController _testingController;
        private Mock<ITestConfigWriter> _fakeTestConfigWriter;

        [SetUp]
        public void SetUp()
        {
            _fakeTestRunner = new Mock<ITestRunner>();
            _fakeProjConfigRepository = new Mock<IProjectConfigurationRepository>();
            _fakeAssemblyDetailsRepository = new Mock<IAssemblyDetailsRepository>();
            _fakeTestConfigWriter = new Mock<ITestConfigWriter>();

            _fakeTestRunner.Setup(x => x.RunTests(ASSEMBLY_PATH)).Returns(new TestResult(new TestName()));

            _testingController = new TestingController(_fakeTestRunner.Object, _fakeProjConfigRepository.Object, _fakeAssemblyDetailsRepository.Object, _fakeTestConfigWriter.Object);
        }

        [Test]
        public void ShouldGetAndUseAssemblyPathForGivenApplication()
        {
            _fakeAssemblyDetailsRepository.Setup(x => x.GetPathFor(APP_NAME)).Returns(ASSEMBLY_PATH);

            _testingController.Get(APP_NAME, "Development");

            _fakeTestRunner.Verify(x => x.RunTests(ASSEMBLY_PATH), Times.Once());
        }

        [Test]
        public void ShouldTriggerWritingOfConfigFileBeforeTestsRun()
        {
            _testingController.Get(APP_NAME, "Development");

            _fakeTestConfigWriter.Verify(x => x.WriteConfigFor(APP_NAME), Times.Once());
        }
    }
}