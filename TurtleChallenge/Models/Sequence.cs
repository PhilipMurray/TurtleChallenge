using Newtonsoft.Json;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    public class Sequence
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("moveOperations")]
        public List<string> MoveOperations { get; set; }
    }
}