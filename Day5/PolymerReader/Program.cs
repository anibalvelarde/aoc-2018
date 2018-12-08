using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymerReader
{
    class Program
    {
        static void Main(string[] args)
        {
            FindCollapsedPolymer();
            OptimizePolymerCollapse();
        }

        private static void OptimizePolymerCollapse()
        {
            var toRemove = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string polymer = File.ReadAllText(@"data\PolymerData.txt");
            long initialUnits = polymer.Count();
            var reduxResult = new Dictionary<char, int>();

            foreach (char removeThis in toRemove)
            {
                var pr = new PolymerReader.Lib.PolymerReader(polymer, removeThis);
                var reactedPolymer = pr.Trigger();

                reduxResult.Add(removeThis, reactedPolymer.Length);
            }

            KeyValuePair<char, int> seedKey = new KeyValuePair<char, int>('\0', int.MaxValue);
            var optimal = reduxResult.Aggregate(seedKey, (optimalObs, nextObs) =>
            {
                return nextObs.Value < optimalObs.Value ? nextObs : optimalObs;
            });
            Console.WriteLine($"The key [{optimal.Key}] yields the smallest polymer units at [{optimal.Value}].");
            Console.ReadKey();
        }

        private static void FindCollapsedPolymer()
        {
            string polymer = File.ReadAllText(@"data\PolymerData.txt");
            long initialUnits = polymer.Count();
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"Original polymer has [{initialUnits}] units.");
            Console.WriteLine($"The resulting polymer has [{reactedPolymer.Length}] units.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("    P   O   L   Y   M   E   R");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(reactedPolymer);
            Console.WriteLine("------------------------------------------------------");
            Console.ReadKey();
        }
    }
}
