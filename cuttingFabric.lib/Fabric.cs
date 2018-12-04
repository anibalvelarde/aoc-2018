using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cuttingFabric.lib
{
    public class Fabric
    {
        private readonly SquareInch[,] _area;

        public Fabric(int length)
        {
            _area = new SquareInch[length,length];
        }

        public int SquareInches
        {
            get
            {
                return _area.Length;
            }
        }

        public void AddClaim(Claim c)
        {
            for (int x = c.claimOffset.x; x < c.lengthOffset; x++)
            {
                for (int y = c.claimOffset.y; y < c.widthOffset; y++)
                {
                    if (_area[x, y] is null) _area[x, y] = new SquareInch(x, y);
                    _area[x, y].AddClaimId(c.id);
                }
            }
        }

        public int CalculateOverlapingSquareInches()
        {
            var t = 0;
            foreach (var item in _area)
            {
                if (item != null)
                {
                    t += item.OverlapingCount;
                }
            }
            return t;
        }

        public bool DoesItOverlap(Claim c)
        {
            bool hasOverlap = false;

            for (int x = c.claimOffset.x; x < c.lengthOffset; x++)
            {
                for (int y = c.claimOffset.y; y < c.widthOffset; y++)
                {
                    if (_area[x, y].OverlapingCount > 0)
                    {
                        hasOverlap = true;
                        break;
                    }
                }
                if (hasOverlap) break;
            }

            return hasOverlap;
        }
    }
}
