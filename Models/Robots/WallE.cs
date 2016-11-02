using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class WallE : WorkingUnit, IWorkingUnit, ITestingUnit
    {
        public WallE() :
            base(String.Empty, 2.0f)
        {

        }
        public WallE(string name) :
            base(name, 2.0f)
        {
            BuildTime = 4.0f;
            Model = "WallE";
        }
    }
}
