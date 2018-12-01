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
        static void Main(string[] args)
        {
            var listAsString = File.ReadAllLines(@"data\calibrationReadings.txt");
            var list = listAsString.Select(x => Convert.ToInt32(x)).ToList();
            var resultingFrequency = list.Aggregate(0, (int newFreq, int curFreq) => curFreq + newFreq);
            Console.WriteLine($"Result {resultingFrequency.ToString()}");
            Console.Write("Press Any Key...");
            Console.ReadKey();
        }
    }
}
