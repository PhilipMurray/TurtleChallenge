using Newtonsoft.Json;

namespace TurtleChallenge.Models
{
    public class ExitPoint
    {
        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}