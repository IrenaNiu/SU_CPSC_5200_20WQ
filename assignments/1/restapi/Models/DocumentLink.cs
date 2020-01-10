using Newtonsoft.Json;

namespace restapi.Models
{
    public class DocumentLink
    {
        public Method Method { get; set; }

        public string Type { get; set; }

        [JsonProperty("rel")]
        public DocumentRelationship Relationship { get; set; }

        [JsonProperty("href")]
        public string Reference { get; set; }
    }
}