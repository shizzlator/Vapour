using System.Collections.Generic;
using MongoDB.Bson;

namespace Vapour.Domain
{
    public class ProjectConfiguration
    {
        public ObjectId Id { get; set; }
        public string ProjectName { get; set; }
        public string Environment { get; set; }
        public IDictionary<string, string> ConfigurationCollection { get; set; }
    }
}