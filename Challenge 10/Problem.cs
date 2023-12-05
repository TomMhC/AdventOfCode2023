using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_10
{
    internal class Problem
    {
        public List<Map> Maps { get; set; } = new List<Map>();

        public List<Tuple<Int64, Int64>> Seeds { get; set; } = new List<Tuple<Int64, Int64>>(); 
    }
}
