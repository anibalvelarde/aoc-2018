using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cuttingFabric.lib
{
    public class Claim
    {
        public Claim(string input)
        {
            InitClaim(input);
        }
        public int id { get; private set; }
        public int width { get; private set; }
        public int length { get; private set; }
        public OffSet claimOffset { get; private set; }
        public int widthOffset
        {
            get
            {
                return width + claimOffset.y;
            }
        }
        public int lengthOffset
        {
            get
            {
                return length + claimOffset.x;
            }
        }

        private void InitClaim(string input)
        {
            var pattern = MakePattern(input);
            this.id = GetIdentifier(pattern);
            this.claimOffset = GetOffset(pattern);
            this.width = GetWidth(pattern);
            this.length = GetLength(pattern);
        }

        private int GetLength(Match pattern)
        {
            return int.Parse(pattern.Groups["length"].Value);
        }

        private int GetWidth(Match pattern)
        {
            return int.Parse(pattern.Groups["width"].Value);
        }

        private OffSet GetOffset(Match pattern)
        {
            var x = int.Parse(pattern.Groups["xOffset"].Value);
            var y = int.Parse(pattern.Groups["yOffset"].Value);
            return new OffSet(x, y);
        }

        private int GetIdentifier(Match pattern)
        {
            return int.Parse(pattern.Groups["id"].Value);
        }

        private Match MakePattern(string input)
        {
            Regex pattern = new Regex(@"#(?<id>\d+) @ (?<yOffset>\d+),(?<xOffset>\d+): (?<width>\d+)x(?<length>\d+)");
            return pattern.Match(input);
        }


    }
}
