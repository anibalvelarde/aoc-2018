using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classification.Lib;

namespace Classification.Main
{
    class Program
    {
        static List<Sample> _sampleData = new List<Sample>();
        static string[] _program;
        static List<string> _whiteList = new List<string>();
        static void Main(string[] args)
        {
            LoadSampleData();

            SearchForOpCodes();

            RunProgram();

        }

        private static void RunProgram()
        {
            LoadProgram();
            var cpu = new Cpu(new int[] { 0, 0, 0, 0 });
            foreach (var iData in _program)
            {
                var inst = new Instruction(iData);
                cpu.ExecuteInstruction(inst.Word);
            }

            Console.WriteLine("===============================================");
            Console.WriteLine($"CPU Registers  r0: {cpu.GetRegisters()[0]}  r1: {cpu.GetRegisters()[1]}  r2:  {cpu.GetRegisters()[2]}  r3:  {cpu.GetRegisters()[3]}");
            Console.WriteLine("===============================================");
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void SearchForOpCodes()
        {
            foreach (var s in _sampleData)
            {
                var cpu = new Cpu(s.RegisterBefore);
                foreach (var op in cpu.Operations)
                {
                    if (!_whiteList.Contains(op.Key))
                    {
                        var answer = cpu.TryExecuteInstruction(
                    MakeCopyOfCurrentRegisters(s.RegisterBefore),
                    s.Instruction,
                    op.Value);
                        if (answer.SequenceEqual(s.RegisterAfter)) s.MatchingOperations.Add(op.Key);

                    }
                }
            }

            var threeOrMoreMatches = _sampleData
                    .Where(x => x.MatchingOperations.Count >= 3)
                    .Count();
            var exactlyOneMatch = _sampleData
                    .Where(x => x.MatchingOperations.Count == 1)
                    .ToList();

            Console.WriteLine($"There were [{threeOrMoreMatches}] samples where Ops behaved similarly.");
            Console.WriteLine($"Out of [{_sampleData.Count}] samples.");
            Console.WriteLine($"There were [{exactlyOneMatch.Count}] samples where 1 Op behaved correctly. Op Code [{exactlyOneMatch.First()}]");
            Console.WriteLine($"Out of [{_sampleData.Count}] samples.");

            OpsWithOneMatch();

            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void OpsWithOneMatch()
        {
            var opsWithOneMatch = _sampleData.Aggregate(new Dictionary<string, KeyValuePair<string, int>>(), (l, x) =>
            {
                if (x.MatchingOperations.Count.Equals(1))
                {
                    var key = x.MatchingOperations.First();
                    if (!l.ContainsKey(key))
                    {
                        l.Add(key, new KeyValuePair<string, int>(key, x.Instruction[0]));
                    }
                }
                return l;
            }).OrderBy(x => x.Key);

            foreach (var op in opsWithOneMatch)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"The key [{op.Key}]  matched the op Code [{op.Value.Value}]");
                Console.WriteLine("---------------------------------------------");
            }
        }

        private static void OpsWithTwoMatches()
        {
            var opsWithTwoMatches = _sampleData.Aggregate(new Dictionary<string, KeyValuePair<string, int>>(), (l, x) =>
            {
                if (x.MatchingOperations.Count.Equals(3) && x.MatchingOperations.Contains("stir"))
                {
                    var key = x.MatchingOperations.First();
                    if (!l.ContainsKey(key))
                    {
                        l.Add(key, new KeyValuePair<string, int>(key, x.Instruction[0]));
                    }
                }
                return l;
            }).OrderBy(x => x.Key);

            foreach (var op in opsWithTwoMatches)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"The key [{op.Key}]  matched the op Code [{op.Value.Value}]");
                Console.WriteLine("---------------------------------------------");
            }
        }

        private static void LoadSampleData()
        {
            var sampleData = File.ReadAllLines(@"data\sampleRegisterData.txt");
            var leadIdx = 0;

            while (leadIdx < sampleData.Length)
            {
                var s = new Sample(sampleData[leadIdx], sampleData[leadIdx + 1], sampleData[leadIdx + 2]);
                _sampleData.Add(s);
                leadIdx += 4;
            }

            _whiteList.Add("opEqir");
            _whiteList.Add("opEqri");
            _whiteList.Add("opEqrr");
            _whiteList.Add("opGtri");
            _whiteList.Add("opGtrr");
            _whiteList.Add("opGtir");
            _whiteList.Add("setr");
            _whiteList.Add("opBani");
            _whiteList.Add("opBanr");
            _whiteList.Add("seti");
            _whiteList.Add("opMulr");
            _whiteList.Add("opBorr");
            _whiteList.Add("opBori");
            _whiteList.Add("addi");
        }

        private static void LoadProgram()
        {
            _program = File.ReadAllLines(@"data\smallProgram.txt");

        }

        static private int[] MakeCopyOfCurrentRegisters(int[] registers)
        {
            int[] copyOfRegisters = { registers[0], registers[1], registers[2], registers[3] };
            return copyOfRegisters;
        }
    }
}
