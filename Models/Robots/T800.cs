using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class T800 : WorkingUnit, IWorkingUnit, ITestingUnit
    {
        public T800() :
            base(String.Empty, 3.0f)
        {

        }
        public T800(string name) :
            base(name, 3.0f)
        {
            BuildTime = 10.0f;
            Model = "T800";
        }
    }
}
