using Newtonsoft.Json;

namespace TurtleChallenge.Models
{
    public class StartingPoint
    {
        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}