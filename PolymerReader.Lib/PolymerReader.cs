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
            this.polymer = this.CleanUpPolymer(polymer);
        }

        public string Trigger()
        {
            var polymerFrag = this.polymer.ToCharArray();
            var length = polymerFrag.Length;
            var sb = new StringBuilder();
            var lastUnit = '\0';
            var annihilations = 0; var noAnnihilations = 0;

            for (int i = 1; i < length; i++)
            {
                if (!annihilate(lastUnit.Equals(_nullUnit) ? polymerFrag[i - 1] : lastUnit, polymerFrag[i]))
                {
                    if (sb.Length.Equals(0) && i<=1) sb.Append(polymerFrag[i - 1]);
                    sb.Append(polymerFrag[i]);
                    lastUnit = polymerFrag[i];
                    noAnnihilations++;
                } else
                {
                    annihilations++;
                    if (lastUnit.Equals(_nullUnit))
                    {
                        i++;
                        if (i.Equals(length - 1))
                        {
                            sb.Append(polymerFrag[i]);
                        }
                    } else
                    {
                        sb.Remove(sb.Length - 1, 1);
                        if (sb.Length.Equals(0))
                        {
                            lastUnit = _nullUnit;
                        } else
                        {
                            lastUnit = sb.ToString().Last();
                        }
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"With length: [{length}]:");
            Console.WriteLine($"o   Had [{annihilations}] annihilations.");
            Console.WriteLine($"o   Had [{noAnnihilations}] non-annihilations.");
            Console.WriteLine("---------------------------------------------------");
            return sb.ToString();
        }

        private bool annihilate(char a, char b)
        {
            bool willAnnihilate = false;
            if (_lowerPolarity.ContainsKey(a))
            {
                willAnnihilate = _lowerPolarity[a].Equals(b);
            }
            if (_upperPolarity.ContainsKey(a))
            {
                willAnnihilate = _upperPolarity[a].Equals(b);
            }
            return willAnnihilate;
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

        private string CleanUpPolymer(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
