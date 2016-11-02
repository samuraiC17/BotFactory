using BotFactory.Common.Tools;
using BotFactory.Common.Interface;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Common;

namespace BotFactory.Factories
{
    public class UnitFactory : IUnitFactory
    {
        public List<IFactoryQueueElement> Queue { get; set; }
        public List<ITestingUnit> Storage { get; set; }

        public FactoryProgressEventHandler FactoryProgress { get; set; }
        public int QueueCapacity { get; set; }
        public int StorageCapacity { get; set; }
        public TimeSpan QueueTime { get; set; }
        public int QueueFreeSlots { get; set; }
        public int StorageFreeSlots { get; set; }

        private static object isBusy = new Object();


        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            Queue = new List<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();
            QueueCapacity = queueCapacity;
            StorageCapacity = storageCapacity;
            QueueFreeSlots = queueCapacity;
            StorageFreeSlots = storageCapacity;
        }

        public async Task<bool> AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            FactoryQueueElement queuedElement = null;
            if (QueueFreeSlots > 0)
            {
                queuedElement = new FactoryQueueElement()
                {
                    Name = name,
                    Model = model,
                    ParkingPos = parkingPos,
                    WorkingPos = workingPos
                };
                Queue.Add(queuedElement);
                QueueFreeSlots--;
            }
            var result = await Task.Run(() =>
              {
                  lock (isBusy)
                  {
                      return AddWorkableUnitToStorage(queuedElement);
                  }
              });

            if (Queue.Contains(queuedElement))
                RemoveFromQueue(queuedElement);

            return result;
        }

        private void RemoveFromQueue(FactoryQueueElement queuedElement)
        {
            Queue.Remove(queuedElement);
            QueueFreeSlots++;
        }

        private async Task<bool> AddWorkableUnitToStorage(FactoryQueueElement queuedElement)
        {
            var result = await Task.Run(async delegate
          {
              ITestingUnit newUnit = null;

              if (StorageFreeSlots > 0 && queuedElement != null)
              {
                  newUnit = (ITestingUnit)Activator.CreateInstance(queuedElement.Model);
                  await Task.Delay(TimeSpan.FromSeconds(newUnit.BuildTime));
                  newUnit.ParkingPos = queuedElement.ParkingPos;
                  newUnit.WorkingPos = queuedElement.WorkingPos;
                  newUnit.CurrentPos = queuedElement.ParkingPos;
                  newUnit.Name = queuedElement.Name;
                  newUnit.Model = queuedElement.Model.Name;

                  Storage.Add(newUnit);
                  StorageFreeSlots--;
                  RemoveFromQueue(queuedElement);
                  return true;
              }
              return false;
          });
            return result;
        }
    }
}
