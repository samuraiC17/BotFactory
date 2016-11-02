using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Reporting
{
    public class StatusChangedEventArgs : EventArgs, IStatusChangedEventArgs
    {
        public StatusChangedEventArgs()
        {

        }
        public StatusChangedEventArgs(string newStatus)
        {
            NewStatus = newStatus;
        }
        public string NewStatus { get; set; }
    }
}
