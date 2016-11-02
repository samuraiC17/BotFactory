using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class WorkingUnit : BaseUnit, IWorkingUnit
    {
        public Coordinates ParkingPos { get; set; }
        public Coordinates WorkingPos { get; set; }
        public bool IsWorking { get; set; }

        public WorkingUnit(string name, float vitesse = 1) :
            base(name, vitesse)
        {

        }

        public virtual async Task<bool> WorkBegins()
        {
            var result = await Move(CurrentPos, WorkingPos);
            return result;
        }

        public virtual async Task<bool> WorkEnds()
        {
            var result = await Move(CurrentPos, ParkingPos);
            return result;

        }
    }
}
