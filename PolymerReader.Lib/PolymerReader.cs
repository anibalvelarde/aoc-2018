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
            var annihilations = 0; var noAnnihilations = 0;
            var lastUnit = _nullUnit;
            var topOfStack = _nullUnit;

            var pStack = new Stack<char>(polymerFrag);
            var gStack = new Stack<char>();

            while (pStack.Count>0)
            {
                var stack = new string(gStack.ToArray());
                var currentUnit = pStack.Pop();

                if (!lastUnit.Equals(_nullUnit))
                {
                    if (annihilate(lastUnit, currentUnit))
                    {
                        annihilations++;
                        lastUnit = _nullUnit;
                        currentUnit = _nullUnit;
                    } else
                    {
                        noAnnihilations++;
                        gStack.Push(lastUnit);
                        topOfStack = lastUnit;
                        lastUnit = currentUnit;
                        if (pStack.Count.Equals(0)) gStack.Push(lastUnit);
                    }
                } else
                {
                    lastUnit = currentUnit;
                    if (topOfStack.Equals(_nullUnit))
                    {
                        noAnnihilations++;
                    } else
                    {
                        if(annihilate(topOfStack,lastUnit))
                        {
                            annihilations++;
                            gStack.Pop();
                           if (gStack.Count>0) topOfStack = gStack.Peek();
                            lastUnit = _nullUnit;
                            currentUnit = _nullUnit;
                        } else
                        {
                            noAnnihilations++;
                            gStack.Push(lastUnit);
                            topOfStack = lastUnit;
                            lastUnit = _nullUnit;
                            currentUnit = _nullUnit;
                        }
                    }
                }
            }

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"With length: [{length}]:");
            Console.WriteLine($"o   Had [{annihilations}] annihilations.");
            Console.WriteLine($"o   Had [{noAnnihilations}] non-annihilations.");
            Console.WriteLine("---------------------------------------------------");

            return new string(gStack.ToArray());
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

        private string ReverseString(string polymer)
        {
            var a = polymer.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }
    }
}
