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
        List<IFactoryQueueElement> Queue { get; }
        List<ITestingUnit> Storage { get; }
        int QueueCapacity { get; set; }
        int StorageCapacity { get; set; }
        TimeSpan QueueTime { get; set; }

        int QueueFreeSlots { get; set; }
        int StorageFreeSlots { get; set; }

        Task<bool> AddWorkableUnitToQueue(Type model, string name,  Coordinates parkingPos, Coordinates workingPos);

        FactoryProgressEventHandler FactoryProgress { get; set; }
    }
}
