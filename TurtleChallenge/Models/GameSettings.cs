using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Models
{
    public class GameSettings
    {
        [JsonProperty("board-size")]
        public BoardSize BoardSize { get; set; }

        [JsonProperty("starting-point")]
        public StartingPoint StartingPoint { get; set; }

        [JsonProperty("direction")]
        public Direction Direction { get; set; }

        [JsonProperty("exit-point")]
        public ExitPoint ExitPoint { get; set; }

        [JsonProperty("mines")]
        public List<Mine> Mines { get; set; }
    }
}
