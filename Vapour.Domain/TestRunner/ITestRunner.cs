using NUnit.Core;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;

namespace Vapour.Domain.TestRunner
{
    public interface ITestRunner
    {
        TestResult RunTests(ProjectConfiguration projectConfiguration);
        TestResult RunTests(ProjectConfiguration projectConfiguration, string testFixtureName);
        TestResult RunTest(ProjectConfiguration projectConfiguration, string testFixtureName, string testMethod);
    }
}