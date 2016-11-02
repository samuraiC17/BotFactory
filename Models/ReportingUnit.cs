using BotFactory.Common;
using BotFactory.Common.Interface;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Reporting
{
    public abstract class ReportingUnit : BuildableUnit, IReportingUnit
    {
        public ReportingUnit()
        {
        }
        
    }
}
