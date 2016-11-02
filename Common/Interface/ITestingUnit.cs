using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{

    public interface ITestingUnit : IReportingUnit
    {
        float Vitesse { get; set; }
        string Name { get; set; }
        Coordinates CurrentPos { get; set; }

        bool Move(Coordinates current, Coordinates target);

        Coordinates ParkingPos { get; set; }
        Coordinates WorkingPos { get; set; }
        bool IsWorking { get; set; }
        bool WorkBegins();
        bool WorkEnds();

    }
}
