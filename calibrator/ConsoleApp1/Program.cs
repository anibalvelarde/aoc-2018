using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static List<int> _FreqList = new List<int>();
        private static bool foundADup = false; 
        private static int loopCount = 0;
        private static int firstRepeat;

        static void Main(string[] args)
        {
            _FreqList.Add(0);
            var listAsString = File.ReadAllLines(@"data\calibrationReadings.txt");
            var list = listAsString.Select(x => Convert.ToInt32(x)).ToList();
            var startingPoint = 0;

            while (!foundADup)
            {
                var resultingFrequency = list.Aggregate(startingPoint, (int accumFreq, int nextFreq) =>
                {
                    var newFreq = (nextFreq + accumFreq);
                    CheckFrequencyForRepeats(newFreq);
                    //Console.WriteLine($"Accum: {accumFreq}  Next: {nextFreq}  New: {newFreq}");
                    return newFreq;
                });
                Console.WriteLine($"Looped through the list [{loopCount++}] times. Resulting frequency: [{resultingFrequency}]");
                startingPoint = resultingFrequency;
            }

            Console.WriteLine($"First repeating frequency: [{firstRepeat}]");
            Console.Write("Press Any Key...");
            Console.ReadKey();
        }

        private static void CheckFrequencyForRepeats(int newFreq)
        {
            if (_FreqList.Any(x => x.Equals(newFreq)))
            {
                Console.WriteLine($"The frequency [{newFreq}] has been seen before.");
                foundADup = true;
                firstRepeat = newFreq;
            }
            _FreqList.Add(newFreq);
        }
    }
}
