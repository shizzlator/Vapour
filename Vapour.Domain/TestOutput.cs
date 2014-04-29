using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Core;

namespace Vapour.Domain
{
    [KnownType(typeof(TestResult))]
    [KnownType(typeof(ArrayList))]
    public class TestOutput
    {
        [DataMember]
        public List<TestResult> TestResult { get; set; }

        [DataMember] 
        public string Message;
    }
}