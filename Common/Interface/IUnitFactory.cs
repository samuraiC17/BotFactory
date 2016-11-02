using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
    public interface IUnitFactory
    {
        List<IFactoryQueueElement> Queue { get; set; }
        List<ITestingUnit> Storage { get; set; }
        int QueueCapacity { get; set; }
        int StorageCapacity { get; set; }
        TimeSpan QueueTime { get; set; }

        int QueueFreeSlots { get;  }
        int StorageFreeSlots { get; }

        bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos);

        FactoryProgressEventHandler FactoryProgress { get; set; }
    }
}
