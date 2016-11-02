using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class R2D2 : WorkingUnit, IWorkingUnit, ITestingUnit
    {
        public R2D2() :
            base(String.Empty, 1.5f)
        {

        }
        public R2D2(string name) :
            base(name, 1.5f)
        {
            BuildTime = 5.5f;
            Model = "R2D2";

        }
    }
}
