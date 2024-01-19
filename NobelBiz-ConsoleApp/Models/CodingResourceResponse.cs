using System.Text.Json.Serialization;

namespace NobelBiz_ConsoleApp.Models
{
    public class CodingResourceResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }

        [JsonPropertyName("topics")]
        public List<string> Topics { get; set; }

        [JsonPropertyName("levels")]
        public List<string> Levels { get; set; }
    }
}
