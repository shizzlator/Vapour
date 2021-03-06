using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vapour.Domain.Models
{
    [DataContract]
    public class ProjectConfiguration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]
        public string Id { get; set; }
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

        [DataMember]
        public bool HasErrors { get; set; }

		public string GetAssemblyPathFor(string assemblyStorePath)
		{
			// No trailing slash
			assemblyStorePath = assemblyStorePath.TrimEnd(@"\".ToCharArray());

			return Path.Combine(assemblyStorePath,
								ProjectName,
								Environment,
								TestDescription,
								AssemblyName +".dll");
		}

		public string GetAssemblyConfigPathFor(string assemblyStorePath)
		{
            return string.Format("{0}.config", GetAssemblyPathFor(assemblyStorePath));
		}
    }
}