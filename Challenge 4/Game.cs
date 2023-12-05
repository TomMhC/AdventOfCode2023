using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_4
{
    internal class Game
    {
        public int Id { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();
        public int MinReds() => this.Sets.Max(s => s.Red);
        public int MinBlues() => this.Sets.Max(s => s.Blue);
        public int MinGreens() => this.Sets.Max(s => s.Green);
        public int Power() => MinReds() * MinBlues() * MinGreens();
        public override string ToString()
        {
            return $"{Id}: R {MinReds()} G {MinGreens()} B {MinBlues()} ";
        }
    }
}
