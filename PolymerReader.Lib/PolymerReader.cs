using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymerReader.Lib
{
    public class PolymerReader
    {
        private string _lowerUnits = "abcdefghijklmnopqrstuvwxyz";
        private string _upperUnits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private char _nullUnit = '\0';

        public string polymer { get; set; }
        private Dictionary<char, char> _upperPolarity;
        private Dictionary<char, char> _lowerPolarity;
        
        public PolymerReader(string polymer)
        {
            LoadPolarityCheckers();
            this.polymer = polymer;
        }

        public string Trigger()
        {
            var polymerFrag = this.polymer.ToCharArray();
            var l = polymerFrag.Length;

            for (int i = 0; i < l; i += 4)
            {
                char prev = polymer[i];
                char current = i < (l - 1) ? polymerFrag[i + 1] : _nullUnit;
                char next = i < (l - 2) ? polymerFrag[i + 2] : _nullUnit;
                char nextNext = i < (l - 3) ? polymerFrag[i + 3] : _nullUnit;

                if (annihilate(current,next))
                {
                    polymerFrag[i + 1] = _nullUnit;
                    polymerFrag[i + 2] = _nullUnit;

                    if (annihilate(prev, nextNext))
                    {
                        polymerFrag[i] = _nullUnit;
                        polymerFrag[i + 3] = _nullUnit;
                    }
                } else if (annihilate(prev, current))
                {
                    polymerFrag[i] = _nullUnit;
                    polymerFrag[i + 1] = _nullUnit;

                    if (annihilate(next, nextNext))
                    {
                        polymerFrag[i + 2] = _nullUnit;
                        polymerFrag[i + 3] = _nullUnit;
                    }
                } 
            }

            return new string(polymerFrag).Replace('\0'.ToString(), string.Empty);
        }

        private bool annihilate(char a, char b)
        {
            if (_lowerPolarity.ContainsKey(a))
            {
                return _lowerPolarity[a].Equals(b);
            }
            if (_upperPolarity.ContainsKey(b))
            {
                return _upperPolarity[b].Equals(a);
            }
            return false;
        }

        private void LoadPolarityCheckers()
        {
            var lower = this._lowerUnits.ToCharArray();
            var upper = this._upperUnits.ToCharArray();

            _lowerPolarity = lower.Select(lowUnit => lowUnit)
                .ToDictionary(k => k, v => upper.First(x => {
                    //Console.WriteLine($"x is [{x}] x.ToLower is [{x.ToString().ToLower()}] v is [{v}]");
                    return x.ToString().ToLower().Equals(v.ToString());
                }));
            _upperPolarity = upper.Select(upUnit => upUnit)
                .ToDictionary(k => k, v => lower.First(x => {
                    //Console.WriteLine($"x is [{x}] x.ToLower is [{x.ToString().ToUpper()}] v is [{v}]");
                    return x.ToString().ToUpper().Equals(v.ToString());
                }));
        }
    }
}
