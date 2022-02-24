using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Amanda.Model
{
    public class PerformanceSetting
    {
        public List<string> AppName { get; set; } = new List<string>();
        public int PoolIntervalSecond { get; set; }
    }
}
