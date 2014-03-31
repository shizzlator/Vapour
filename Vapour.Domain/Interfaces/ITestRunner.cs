using NUnit.Core;

namespace Vapour.Domain.Interfaces
{
    public interface ITestRunner
    {
        TestResult RunTests(string pathToAssembly);
        TestResult RunTests(string pathToAssembly, string testFixture);
        TestResult RunTest(string pathToAssembly, string testMethod);
    }
}