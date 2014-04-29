using NUnit.Core;
using TestOutput = Vapour.Domain.TestOutput;

namespace Vapour.API
{
    public interface ITestOutputFactory
    {
        TestOutput Create(TestResult testResult);
    }
}