using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checksum.lib
{
    public class StringyComparer
    {
        private string _s1;
        private string _s2;

        public StringyComparer(string s1, string s2)
        {
            _s1 = s1;
            _s2 = s2;
        }

        public int SameCharacterCount()
        {
            return 0;
        }

        public int DifferentCharacterCount()
        {
            var arr1 = _s1.ToCharArray();
            var arr2 = _s2.ToCharArray();

            switch (_s1.Length >= _s2.Length)
            {
                case true:
                    return FindDifferences(arr1, arr2);

                case false:
                    return FindDifferences(arr2, arr1);

                default:
                    return 0;
            }
        }

        private int FindDifferences(char[] a1, char[] a2)
        {
            var differences = a1.Except(a2);
            return differences.Count();
        }

        public string ExtractCommonCharacters(bool anyPosition = false)
        {
            if (this.DifferentCharacterCount().Equals(0))
            {
                return this._s1;
            } else
            {
                if (anyPosition)
                {
                    return CommonAtAnyPosition();

                } else
                {
                    return CommonAtSinglePosition();
                }
            }
        }

        private string CommonAtAnyPosition()
        {
            var arr1 = _s1.ToCharArray();
            var arr2 = _s2.ToCharArray();

            var result = new string(arr1.Intersect(arr2).ToArray());
            return result;
        }

        private string CommonAtSinglePosition()
        {
            var arr1 = _s1.ToCharArray();
            var arr2 = _s2.ToCharArray();
            List<int> arrDiffIdx = new List<int>();

            for (int i = 0; i < (arr1.Length-1); i++)
            {
                if (arr1[i] != arr2[i])
                {
                    arrDiffIdx.Add(i);
                }
            }

            if (arrDiffIdx.Count().Equals(1))
            {
                var element = arr1[arrDiffIdx.First()];
                var agg = arr1.Aggregate(new List<char>(), (accum, item) =>
                {
                    if (item != element) accum.Add(item);
                    return accum;
                }).ToArray();
                var result = new string(agg);
                return result;
            }

            return "";
        }
    }
}
