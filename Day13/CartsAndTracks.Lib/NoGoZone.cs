using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartsAndTracks.Lib
{
    public class NoGoZone : GridPoint
    {
        public NoGoZone(int x, int y)
            :base (x, y) { }

        public NoGoZone(Coordinates pos)
            : base(pos.Col, pos.Row) { }

        public override bool IsPavedWhenHeading(Heading direction)
        {
            return false;
        }

        public override string Render()
        {
            return "";
        }
    }
}
