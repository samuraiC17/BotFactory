using BotFactory.Common.Tools;
using BotFactory.Common.Interface;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Common;
using System.Threading;

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
        public int QueueFreeSlots { get { return QueueCapacity - Queue.Count; } }
        public int StorageFreeSlots { get { return StorageCapacity - Storage.Count; } }

        private static object isBusy = new Object();


        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            Queue = new List<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();
            QueueCapacity = queueCapacity;
            StorageCapacity = storageCapacity;
        }

        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            FactoryQueueElement queuedElement = null;
            lock (isBusy)
            {

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
                }
            }

            bool result = false;
            Thread thread = new Thread(() =>
            {
                lock (isBusy)
                {
                    result = AddWorkableUnitToStorage(queuedElement);
                }
            });

            thread.Start();

            if (Queue.Contains(queuedElement))
                Queue.Remove(queuedElement);
            return result;
        }

        private bool AddWorkableUnitToStorage(FactoryQueueElement queuedElement)
        {
            ITestingUnit newUnit = null;
            Thread thread = new Thread(() =>
            {
                if (StorageFreeSlots > 0 && queuedElement != null)
                {
                    newUnit = (ITestingUnit)Activator.CreateInstance(queuedElement.Model);
                    Thread.Sleep(TimeSpan.FromSeconds(newUnit.BuildTime));
                    newUnit.ParkingPos = queuedElement.ParkingPos;
                    newUnit.WorkingPos = queuedElement.WorkingPos;
                    newUnit.CurrentPos = queuedElement.ParkingPos;
                    newUnit.Name = queuedElement.Name;
                    newUnit.Model = queuedElement.Model.Name;

                    Storage.Add(newUnit);
                    Queue.Remove(queuedElement);
                }
            });
            thread.Start();
            return newUnit != null;
        }
    }
}
