using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartsAndTracks.Lib;
using System.Windows.Forms;

namespace CartsAndTracks.Main
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            // arrange
            int ticksLimit = 100000;
            var t = new Track(@"data\foRealData.txt", ticksLimit: ticksLimit);
            t.Load();

            // act
            t.Simulate();

            // assert
            Clipboard.SetText(t.Render());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.CrashReport());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.Carts[0].ToString());
            Console.WriteLine($"  Total Ticks: {t.TotalTicks}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
