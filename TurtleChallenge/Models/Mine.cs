using Newtonsoft.Json;

namespace TurtleChallenge.Models
{
    public class Mine
    {
        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}