using NUnit.Core;
using Vapour.Domain.TestRunner;

namespace Vapour.API.Models
{
    public interface ITestOutputModelFactory
    {
        TestOutputModel Create(TestResult testResult);
    }
}