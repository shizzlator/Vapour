using NUnit.Core;

namespace Vapour.Domain.Interfaces
{
    public interface ITestRunner
    {
        TestResult RunTests(ProjectConfiguration projectConfiguration);
        TestResult RunTests(ProjectConfiguration projectConfiguration, string testFixtureName);
        TestResult RunTest(ProjectConfiguration projectConfiguration, string testFixtureName, string testMethod);
    }
}