using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_5
{
    internal class Part
    {
        public Point Start { get; set; }
        public int Id { get; set; }
        public Point End
        {
            get
            {
                var nr = (Id.ToString()).Length - 1;
                return new Point(Start.X + nr, Start.Y);
            }
        }

        public bool IsAdjacent(Point p)
        {
            if (p.Y < Start.Y - 1) return false;
            if (p.Y > Start.Y + 1) return false;
            if (p.X < Start.X - 1) return false;
            if (p.X > End.X + 1) return false;

            return true;
        }
    }
}
