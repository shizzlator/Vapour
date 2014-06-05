using System.Runtime.Serialization;

namespace Vapour.API.Models
{
    public class ArtifactDownloadOutputModel
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}