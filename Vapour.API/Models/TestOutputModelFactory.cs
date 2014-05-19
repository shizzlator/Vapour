using System.Collections.Generic;
using System.Linq;
using NUnit.Core;
using Vapour.Domain.TestRunner;

namespace Vapour.API.Models
{
    public class TestOutputModelFactory : ITestOutputModelFactory
    {
        public TestOutputModel Create(TestResult testResult)
        {
            if (testResult.IsSuccess)
                return new TestOutputModel { Message = "All Tests Passed!",  Success = testResult.IsSuccess };

            return new TestOutputModel { Message = "Some Tests Failed!", Success = testResult.IsSuccess, TestResult = CreateTestResultModel(GetFailedTestResults(GetTestResults(testResult))) };
        }

        private List<TestResult> GetFailedTestResults(List<TestResult> testResults)
        {
            return testResults.Where(testResult => testResult.IsFailure).ToList();
        }

        private static List<TestResult> GetTestResults(TestResult result)
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

        private static List<TestResultModel> CreateTestResultModel(List<TestResult> testResults)
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