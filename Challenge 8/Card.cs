using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_8
{
    internal class Card
    {
        public List<int> Winning { get; set; } = new List<int>();
        public List<int> Numbers { get; set; } = new List<int>();

        public int Won()
        {
            var rslt = 0;

            foreach (var n in Numbers)
            {
                if (Winning.Contains(n))
                {
                    rslt++;
                }
            }

            return rslt;
        }

        public int Val()
        {
            var rslt = 0;

            foreach(var n in Numbers)
            {
                if (Winning.Contains(n))
                {
                    if (rslt == 0)
                        rslt = 1;
                    else
                        rslt *= 2;
                }
            }

            return rslt;
        }
    }
}
