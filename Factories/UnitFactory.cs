using BotFactory.Common.Tools;
using BotFactory.Common.Interface;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Common;
using BotFactory.Reporting;

namespace BotFactory.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private List<IFactoryQueueElement> _queue;
        public List<IFactoryQueueElement> Queue
        {
            get
            {
                return _queue.ToList();
            }
        }

        private List<ITestingUnit> _storage;

        public List<ITestingUnit> Storage
        {
            get
            {
                return _storage.ToList();
            }
        }

        public FactoryProgressEventHandler FactoryProgress { get; set; }
        public int QueueCapacity { get; set; }
        public int StorageCapacity { get; set; }
        public TimeSpan QueueTime { get; set; }
        public int QueueFreeSlots { get; set; }
        public int StorageFreeSlots { get; set; }

        private static object isBusy = new Object();


        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            _queue = new List<IFactoryQueueElement>();
            _storage = new List<ITestingUnit>();
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
                _queue.Add(queuedElement);
                OnUnitStatusChanged(new StatusChangedEventArgs() { NewStatus = String.Format("Element on Queue => {0}", _queue.First().Name) });
                QueueFreeSlots--;
            }
            var result = await Task.Run(() =>
              {
                  lock (isBusy)
                  {
                      return AddWorkableUnitToStorage(queuedElement);
                  }
              });

            if (_queue.Contains(queuedElement))
                RemoveFromQueue(queuedElement);

            return result;
        }

        private void OnUnitStatusChanged(StatusChangedEventArgs statusChangedEventArgs)
        {
            if (FactoryProgress != null)
            {
                FactoryProgress(this, statusChangedEventArgs);
            }
        }

        private void RemoveFromQueue(FactoryQueueElement queuedElement)
        {
            _queue.Remove(queuedElement);
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

                  _storage.Add(newUnit);
                  StorageFreeSlots--;
                  RemoveFromQueue(queuedElement);
                  OnUnitStatusChanged(new StatusChangedEventArgs() { NewStatus = String.Format("Element deleted from Queue => {0}", _queue.First().Name) });
                  OnUnitStatusChanged(new StatusChangedEventArgs() { NewStatus = String.Format("Element on _storage => {0}", _storage.First().Name) });
                  return true;
              }
              return false;
          });
            return result;
        }
    }
}
