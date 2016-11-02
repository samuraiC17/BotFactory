using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
    public interface IBaseUnit : IReportingUnit
    {
        float Vitesse { get; set; }
        string Name { get; set; }
        Coordinates CurrentPos { get; set; }

        Task<bool> Move(Coordinates current, Coordinates target);
    }
}
