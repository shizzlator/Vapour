using NUnit.Core;

namespace Vapour.Domain.Interfaces
{
    public interface ITestRunner
    {
        TestResult RunTests(string projectName, string environment, string testDescription);
        TestResult RunTests(string projectName, string environment, string testDescription, string testFixtureName);
        TestResult RunTest(string projectName, string environment, string testDescription, string testFixtureName, string testMethod);
    }
}