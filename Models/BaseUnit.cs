using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class BaseUnit : ReportingUnit, IBaseUnit
    {
        public float Vitesse { get; set; }
        public string Name { get; set; }
        public Coordinates CurrentPos { get; set; }

        public BaseUnit(string name, float vitesse = 1)
        {
            Name = name;
        }

        public async Task<bool> Move(Coordinates current, Coordinates target)
        {
            var result = await Task.Run(() =>
               {
                   if ((CurrentPos.Equals(current)
                   && !current.Equals(target)))
                   {
                       Task.Delay((int)Vector.FromCoordinates(current, target).Length);
                       CurrentPos.X = target.X;
                       CurrentPos.Y = target.Y;
                   }
                   return CurrentPos.Equals(target);
               });
            return result;
        }
    }
}
