using System;
using System.Collections.Generic;
using NUnit.Core;
using NUnit.Framework;
using Vapour.API.Models;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class TestObjectFactoryTests
    {
        [Test]
		[Ignore]
        public void should_format_test_output_from_nunit()
        {
            //given
	        TestMock testMock = new TestMock();
			TestResult testResult = new TestResult(testMock);

            //when
            var testOutputModel = new TestOutputModelFactory().Create(testResult);

            // then
            Assert.That(testOutputModel.Message, Is.EqualTo("All Tests Passed!"));
            Assert.That(testOutputModel.Success, Is.True);
            Assert.That(testOutputModel.FailedTests, Is.Null);
        }
    }

	public class TestMock : ITest
	{
		#region ITest Members

		public System.Collections.IList Categories
		{
			get { return new List<string>(); }
		}

		public string ClassName
		{
			get { return "Class name"; }
		}

		public int CountTestCases(ITestFilter filter)
		{
			return 0;
		}

		public string Description { get; set; }

		public string IgnoreReason { get; set; }

		public bool IsSuite
		{
			get { return false; }
		}

		public string MethodName
		{
			get { return "Method name"; }
		}

		public ITest Parent
		{
			get { return null; }
		}

		public System.Collections.IDictionary Properties
		{
			get { return new Dictionary<string, string>(); }
		}

		public RunState RunState { get; set; }

		public int TestCount
		{
			get { return 1; }
		}

		public TestName TestName
		{
			get { return new TestName() { Name = "Test name"}; }
		}

		public string TestType
		{
			get { return "Method"; }
		}

		public System.Collections.IList Tests
		{
			get { return new List<TestResult>(); }
		}

		#endregion
	}
}