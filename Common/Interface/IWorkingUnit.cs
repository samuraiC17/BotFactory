using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
    public interface IWorkingUnit :IBaseUnit
    {
        Coordinates ParkingPos { get; set; }
        Coordinates WorkingPos { get; set; }
        bool IsWorking { get; set; }
        Task<bool> WorkBegins();
        Task<bool> WorkEnds();
    }
}
