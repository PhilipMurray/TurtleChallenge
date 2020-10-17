using Newtonsoft.Json;

namespace TurtleChallenge.Models
{
    public class BoardSize
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}