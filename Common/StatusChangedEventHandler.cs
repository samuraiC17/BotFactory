using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common
{
    public delegate void StatusChangedEventHandler(ITestingUnit bot, IStatusChangedEventArgs args);
}
