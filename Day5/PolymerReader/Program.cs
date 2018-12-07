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
            string contents = File.ReadAllText(@"data\PolymerData.txt");
        }
    }
}
