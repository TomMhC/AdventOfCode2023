using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_10
{
    internal class Map
    {
        private List<Set> _sets = new List<Set>();

        public void Parse(List<string> lines)
        {
            foreach(var line in lines)
            {
                Parse(line);
            }
        }

        public Int64 GetDestination(Int64 source)
        {
            foreach(var s in _sets)
            {
                if (source >= s.Source &&  source < s.Source + s.Range)
                {
                    return s.Destination + (source - s.Source);
                }
            }

            return source;
        }

        private void Parse(string line)
        {
            var values = line.Split(" ");
            var destination = Int64.Parse(values[0]);
            var source = Int64.Parse(values[1]);
            var range = Int64.Parse(values[2]);

            _sets.Add(new Set()
            {
                Source = source,
                Destination = destination,
                Range = range
            });
        }
    }

    internal class Set
    {
        public Int64 Source { get; set; }
        public Int64 Destination { get; set; }
        public Int64 Range { get; set; }
    }
}
