using System.Collections.Generic;

namespace cuttingFabric.lib
{
    public class SquareInch
    {
        private List<int> _ids = new List<int>();
        public int x { get; }
        public int y { get; }

        public SquareInch(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void AddClaimId(int claimId)
        {
            this._ids.Add(claimId);
        }

        public int OverlapingCount
        {
            get
            {
                if (_ids.Count > 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
