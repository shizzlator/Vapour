using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Vapour.API.Models
{
    public class TestOutputModel
    {
        [DataMember]
        public List<TestResultModel> FailedTests { get; set; }

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