using System.Collections.Generic;
using System.Linq;

namespace checksum.lib
{
    public class IdChecker
    {
        private SortedList<int, int> _list = new SortedList<int, int>();

        public IdChecker()
        {
            _list.Add(0, 0);
            _list.Add(2, 0);
            _list.Add(3, 0);
        }

        public void CheckIdForRepeat(string ID, int repeat)
        {
            var idParts = ID.ToCharArray();
            var result = idParts.Aggregate(new SortedList<char, int>(),
                (SortedList<char, int> repeats, char item) =>
                {
                    if (repeats.ContainsKey(item))
                    {
                        repeats[item] = repeats[item] + 1;
                    }
                    else
                    {
                        repeats.Add(item, 1);
                    }
                    return repeats;
                });
            var idx = result.IndexOfValue(repeat);
            if (idx >= 0)
            {
                this._list[repeat] = this._list[repeat] + 1;
            }
        }

        public int GetCheckSum()
        {
            return this.GetCountForRepeats(2) * this.GetCountForRepeats(3);
        }

        public int GetCountForRepeats(int repeatIndex)
        {
            int repeatCount = 0;
            try
            {
                _list.TryGetValue(repeatIndex, out repeatCount);
                return repeatCount;
            }
            catch (KeyNotFoundException)
            {
                return 0;
            }
        }
    }
}
