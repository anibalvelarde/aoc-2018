using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classification.Lib
{
    public class Sample
    {
        public int[] RegisterBefore { get; private set; }
        public int[] Instruction { get; private set; }
        public int[] RegisterAfter { get; private set; }
        public List<string> MatchingOperations
        {
            get
            {
                return _matchingOperations;
            }
        }

        private List<string> _matchingOperations = new List<string>();

        public Sample(string before, string instruction, string after)
        {
            ProcessBefore(before);
            ProcessInstruction(instruction);
            ProcessAfter(after);
        }

        private void ProcessAfter(string afterData)
        {
            var pattern = MakeAfterRegisterPattern(afterData);
            int r1 = int.Parse(pattern.Groups["r1"].Value);
            int r2 = int.Parse(pattern.Groups["r2"].Value);
            int r3 = int.Parse(pattern.Groups["r3"].Value);
            int r4 = int.Parse(pattern.Groups["r4"].Value);

            RegisterAfter = new int[] { r1, r2, r3, r4 };
        }

        private void ProcessInstruction(string instructionData)
        {
            var pattern = MakeInstructionPattern(instructionData);
            int r1 = int.Parse(pattern.Groups["r1"].Value);
            int r2 = int.Parse(pattern.Groups["r2"].Value);
            int r3 = int.Parse(pattern.Groups["r3"].Value);
            int r4 = int.Parse(pattern.Groups["r4"].Value);

            Instruction = new int[] { r1, r2, r3, r4 };
        }

        private void ProcessBefore(string beforeData)
        {
            var pattern = MakeBeforeRegisterPattern(beforeData);
            int r1 = int.Parse(pattern.Groups["r1"].Value);
            int r2 = int.Parse(pattern.Groups["r2"].Value);
            int r3 = int.Parse(pattern.Groups["r3"].Value);
            int r4 = int.Parse(pattern.Groups["r4"].Value);

            RegisterBefore = new int[] { r1, r2, r3, r4 };
        }

        private Match MakeBeforeRegisterPattern(string input)
        {
            // Before: [0, 1, 2, 1]
            Regex pattern = new Regex($@"Before: {Regex.Escape("[")}(?<r1>\d+), (?<r2>\d+), (?<r3>\d+), (?<r4>\d+){Regex.Escape("]")}$");
            return pattern.Match(input);
        }

        private Match MakeInstructionPattern(string input)
        {
            //  13 2 1 3
            Regex pattern = new Regex($@"(?<r1>\d+) (?<r2>\d+) (?<r3>\d+) (?<r4>\d+)$");
            return pattern.Match(input);
        }

        private Match MakeAfterRegisterPattern(string input)
        {
            // After:  [0, 1, 2, 1]
            Regex pattern = new Regex($@"After:  {Regex.Escape("[")}(?<r1>\d+), (?<r2>\d+), (?<r3>\d+), (?<r4>\d+){Regex.Escape("]")}$");
            return pattern.Match(input);
        }
    }
}
