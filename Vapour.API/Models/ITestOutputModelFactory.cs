using NUnit.Core;
using Vapour.Domain;

namespace Vapour.API
{
    public interface ITestOutputModelFactory
    {
        TestOutputModel Create(TestResult testResult);
    }
}