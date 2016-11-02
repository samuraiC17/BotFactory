using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Tools
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }


        public static Vector FromCoordinates(Coordinates begin, Coordinates end)
        {
            return new Vector()
            {
                X = end.X - begin.X,
                Y = end.Y - begin.Y
            };
        }
    }
}
