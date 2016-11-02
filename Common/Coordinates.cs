using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Tools
{
    public class Coordinates
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var target = obj as Coordinates;
            return X == target.X
                && Y == target.Y;
        }
        public Coordinates()
        {
            X = 0.0f;
            Y = 0.0f;
        }
        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()
        {
            return $"{X}_{Y}".GetHashCode();
        }
    }
}
