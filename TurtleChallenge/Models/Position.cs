using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Models
{
    public struct Position
    {
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
