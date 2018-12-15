using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartsAndTracks.Lib
{
    class CrashDetector
    {
        Dictionary<string, List<Cart>> _radar = new Dictionary<string, List<Cart>>();
        public bool CrashDetected { get; private set; }

        public bool DetectCrash(List<Cart> carts)
        {
            var locations = carts.Aggregate(new Dictionary<string, Cart>(), (l, currentCart) =>
            {
                if (!l.ContainsKey(currentCart.CurrentPosition.ToKey()))
                {
                    l.Add(currentCart.CurrentPosition.ToKey(), currentCart);
                } else
                {
                    CrashDetected = true;
                    l.TryGetValue(currentCart.CurrentPosition.ToKey(), out var prevCart);
                    var cartList = new List<Cart>();
                    cartList.Add(currentCart);
                    cartList.Add(prevCart);
                    if (!_radar.ContainsKey(currentCart.CurrentPosition.ToKey()))
                    {
                        _radar.Add(currentCart.CurrentPosition.ToKey(), cartList);
                    }
                    CrashEm(cartList);
                }
                return l;
            });
            return CrashDetected;
        }

        public List<Coordinates> CrashLocations()
        {
            return _radar
                    .Where(x => x.Value.Count > 1)
                    .Select(x => x.Value[0].CurrentPosition)
                    .ToList();
        }

        private void CrashEm(List<Cart> carts)
        {
            foreach (var c in carts)
            {
                c.Crash();
            }
        }
    }
}
