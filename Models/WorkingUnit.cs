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

        public virtual bool WorkBegins()
        {
            return Move(CurrentPos, WorkingPos);
        }

        public virtual bool WorkEnds()
        {
            return Move(CurrentPos, ParkingPos);
        }
    }
}
