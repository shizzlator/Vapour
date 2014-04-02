using MongoDB.Bson;

namespace Vapour.Domain
{
    public class AssemblyDetails
    {
        public ObjectId Id;
        public string AppName { get; set; }
        public string TestDescription { get; set; }
        public string AssemblyName { get; set; }
    }
}