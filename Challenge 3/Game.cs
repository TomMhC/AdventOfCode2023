using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_3
{
    internal class Game
    {
        public int Id { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();
        public int SumReds() => this.Sets.Sum(s => s.Red);
        public int SumBlues() => this.Sets.Sum(s => s.Blue);
        public int SumGreens() => this.Sets.Sum(s => s.Green);
        public override string ToString()
        {
            return $"{Id}: R {SumReds()} G {SumGreens()} B {SumBlues()} ";
        }
    }
}
