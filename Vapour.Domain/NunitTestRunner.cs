using System;
using NUnit.Core;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain
{
    public class NunitTestRunner : ITestRunner
    {
        public TestResult RunTests(string pathToAssembly)
        {
            CoreExtensions.Host.InitializeService();

            var testPackage = new TestPackage(pathToAssembly);
            var remoteTestRunner = new RemoteTestRunner();
            remoteTestRunner.Load(testPackage);


            return remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);
        }

        public TestResult RunTests(string pathToAssembly, string testFixture)
        {
            throw new NotImplementedException();
        }

        public TestResult RunTest(string pathToAssembly, string testMethod)
        {
            throw new NotImplementedException();
        }
    }
}