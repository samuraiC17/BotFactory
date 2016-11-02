using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class HAL : WorkingUnit, IWorkingUnit, ITestingUnit
    {
        public HAL() :
            base(String.Empty, 0.5f)
        {

        }
        public HAL(string name) :
            base(name, 0.5f)
        {
            BuildTime = 7.0f;
            Model = "HAL";
        }
    }
}
