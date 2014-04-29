using System.Collections.Generic;
using System.Linq;
using NUnit.Core;
using TestOutput = Vapour.Domain.TestOutput;

namespace Vapour.API
{
    public class TestOutputFactory : ITestOutputFactory
    {
        public TestOutput Create(TestResult testResult)
        {
            if (testResult.IsSuccess)
                return new TestOutput { Message = "All Tests Passed!" };

            return new TestOutput { Message = "Some Tests Failed", TestResult = GetFailedTestResults(GetTestResults(testResult)) };
        }

        private List<TestResult> GetFailedTestResults(List<TestResult> testResults)
        {
            return testResults.Where(testResult => testResult.IsFailure).ToList();
        }

        private List<TestResult> GetTestResults(TestResult result)
        {
            while (true)
            {
                if (result.HasResults && ((TestResult)result.Results[0]).FailureSite == FailureSite.Test)
                {
                    return result.Results.Cast<TestResult>().ToList();
                }
                result = (TestResult)result.Results[0];
            }
        }
    }
}