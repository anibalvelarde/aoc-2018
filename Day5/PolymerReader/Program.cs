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
