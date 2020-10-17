using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Models
{
    public class Moves
    {
        [JsonProperty("sequences")]
        public List<Sequence> Sequences { get; set; }
    }
}
