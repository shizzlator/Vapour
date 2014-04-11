using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace Vapour.Domain
{
    [DataContract]
    public class ProjectConfiguration
    {
        [IgnoreDataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string Environment { get; set; }
        [DataMember]
        public string TestDescription { get; set; }
        [DataMember]
        public string AssemblyName { get; set; }
        [DataMember]
        public IDictionary<string, string> ConfigurationCollection { get; set; }
    }
}