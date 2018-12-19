using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classification.Lib
{
    public class Instruction
    {
        public int[] _instruction { get; private set; }

        private List<string> _matchingOperations = new List<string>();

        public Instruction(string instruction)
        {
            ProcessInstruction(instruction);
        }

        public int[] Word => _instruction;

        private void ProcessInstruction(string instructionData)
        {
            var pattern = MakeInstructionPattern(instructionData);
            int r1 = int.Parse(pattern.Groups["r1"].Value);
            int r2 = int.Parse(pattern.Groups["r2"].Value);
            int r3 = int.Parse(pattern.Groups["r3"].Value);
            int r4 = int.Parse(pattern.Groups["r4"].Value);

            _instruction = new int[] { r1, r2, r3, r4 };
        }

        private Match MakeInstructionPattern(string input)
        {
            //  13 2 1 3
            Regex pattern = new Regex($@"(?<r1>\d+) (?<r2>\d+) (?<r3>\d+) (?<r4>\d+)$");
            return pattern.Match(input);
        }
    }
}
