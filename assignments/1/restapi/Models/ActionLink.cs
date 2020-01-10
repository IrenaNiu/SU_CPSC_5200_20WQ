using Newtonsoft.Json;

namespace restapi.Models
{
    public class ActionLink
    {
        public Method Method { get; set; }

        public string Type { get; set; }

        [JsonProperty("rel")]
        public ActionRelationship Relationship { get; set; }

        [JsonProperty("href")]
        public string Reference { get; set; }
       
    }
}