using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vapour.Domain
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

		public string GetAssemblyPathFor(string assemblyStorePath)
		{
			return Path.Combine(assemblyStorePath.TrimEnd(@"\".ToCharArray()),
				ProjectName,
				TestDescription,
				Environment,
				AssemblyName +".dll");
		}

		public string GetAssemblyConfigPathFor(string assemblyStorePath)
		{
			return GetAssemblyPathFor(assemblyStorePath) + ".config";
		}

		public static string GetAssemblyPathFor(ProjectConfiguration configuration, string assemblyStorePath)
		{
			return configuration.GetAssemblyPathFor(assemblyStorePath);
		}

		public static string GetAssemblyConfigPathFor(ProjectConfiguration configuration, string assemblyStorePath)
		{
			return configuration.GetAssemblyConfigPathFor(assemblyStorePath);
		}
    }
}