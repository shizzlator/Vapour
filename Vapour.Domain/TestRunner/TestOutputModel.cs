using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Core;

namespace Vapour.Domain
{
    [KnownType(typeof(TestResult))]
    [KnownType(typeof(ArrayList))]
    public class TestOutputModel
    {
        [DataMember]
        public List<TestResultModel> TestResult { get; set; }

        [DataMember] 
        public string Message;

        [DataMember]
        public bool Success { get; set; }
    }

    public class TestResultModel
    {
        public string TestName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}