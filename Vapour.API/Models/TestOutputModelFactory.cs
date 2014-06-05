using System.Collections.Generic;
using System.Linq;
using NUnit.Core;

namespace Vapour.API.Models
{
    public class TestOutputModelFactory : ITestOutputModelFactory
    {
        public TestOutputModel Create(TestResult testResult)
        {
            if (testResult.IsSuccess)
                return new TestOutputModel { Message = "All Tests Passed!",  Success = testResult.IsSuccess };

            return new TestOutputModel { Message = "Some Tests Failed!", Success = testResult.IsSuccess, FailedTests = PoulateFailedTestsCollection(GetFailedTests(GetTestResults(testResult))) };
        }

        private IEnumerable<TestResult> GetFailedTests(IEnumerable<TestResult> testResults)
        {
            return testResults.Where(testResult => testResult.IsFailure).ToList();
        }

        private static IEnumerable<TestResult> GetTestResults(TestResult result)
        {
            while (true)
            {
                if ((result.HasResults && result.Results[0] != null) && ((TestResult)result.Results[0]).FailureSite == FailureSite.Test)
                {
                    return result.Results.Cast<TestResult>().ToList();
                }
                result = (TestResult)result.Results[0];
            }
        }

        private static List<TestResultModel> PoulateFailedTestsCollection(IEnumerable<TestResult> testResults)
        {
            return testResults.Select(testResult => new TestResultModel
            {
                TestName = testResult.FullName,
                Message = testResult.Message,
                StackTrace = testResult.StackTrace
            }).ToList();
        }
    }
}