using System.Collections;
using System.Runtime.Serialization;
using NUnit.Core;

namespace Vapour.Domain
{
    [KnownType(typeof(TestResult))]
    [KnownType(typeof(ArrayList))]
    public class TestOutput
    {
        [DataMember]
        public TestResult TestResult { get; set; }
    }
}