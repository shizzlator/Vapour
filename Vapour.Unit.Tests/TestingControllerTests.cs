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
        private const string PROJECT_NAME = "MyProject";

        private Mock<ITestRunner> _fakeTestRunner;
        private TestingController _testingController;
        private Mock<IAssemblyConfigWriter> _fakeTestConfigWriter;

        [SetUp]
        public void SetUp()
        {
            _fakeTestRunner = new Mock<ITestRunner>();
            _fakeTestConfigWriter = new Mock<IAssemblyConfigWriter>();

            _fakeTestRunner.Setup(x => x.RunTests(ASSEMBLY_PATH)).Returns(new TestResult(new TestName()));

            _testingController = new TestingController(_fakeTestRunner.Object, _fakeTestConfigWriter.Object);
        }

    }
}