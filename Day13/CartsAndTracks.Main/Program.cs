using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartsAndTracks.Lib;

namespace CartsAndTracks.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            // arrange
            int ticksLimit = 100000;
            var t = new Track(@"data\foRealData.txt");
            t.Load();

            // act
            t.Simulate(ticksLimit);

            // assert
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.CrashReport());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
