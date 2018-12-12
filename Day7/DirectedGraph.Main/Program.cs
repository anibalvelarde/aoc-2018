using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectedGraph.Lib;

namespace DirectedGraph.Main
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            // Arange
            var statements = File.ReadAllLines(@"data\Day7data.txt");
            var g = new Graph();
            var statementsCount = 0;

            foreach (var statement in statements)
            {
                // Act
                g.AddStatement(statement);
                statementsCount++;
            }

            var steps = g.GetPrecedenceSequence(5, 60);
            Clipboard.SetText(steps);
            Console.WriteLine($"The order of steps should be: [{steps}].");
            Console.WriteLine($"There were [{statementsCount}] processed.");
            Console.ReadKey();
        }
    }
}
