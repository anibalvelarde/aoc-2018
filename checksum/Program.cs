using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checksum.lib;
using System.Windows.Forms;

namespace checksum
{
    class Program
    {
        private static string[] _listOfIDs = File.ReadAllLines(@"data\listOfIds.txt");

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            ComputeCheckSum();
            FindPrototypeFabricBox();
        }

        private static void FindPrototypeFabricBox()
        {
            bool exitLoop = false;

            foreach (var boxId in _listOfIDs)
            {
                foreach (var boxToCheck in _listOfIDs)
                {
                    var checker = new StringyComparer(boxId, boxToCheck);
                    if (checker.DifferentCharacterCount().Equals(1))
                    {
                        var diff = checker.ExtractCommonCharacters();
                        if (diff.Length>0)
                        {
                            Clipboard.SetText(diff);
                            Console.WriteLine();
                            Console.WriteLine($"The Box: [{boxId}] and ");
                            Console.WriteLine($"The Box: [{boxToCheck}] differ by 1 character.");
                            Console.WriteLine($"The Dif: [{diff}]. It was copied to the clipboard.");
                            //exitLoop = true;
                            //break;
                        }                    }
                }
                if (exitLoop) break;
            }
            Console.Write("Press any key..."); Console.ReadKey();
        }

        private static void ComputeCheckSum()
        {
            var checker = new IdChecker();
            foreach (var id in _listOfIDs)
            {
                checker.CheckIdForRepeat(id, 2);
                checker.CheckIdForRepeat(id, 3);
            }

            Console.WriteLine($"{_listOfIDs.Count()} IDs found. Check Sum: {checker.GetCheckSum()}");
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
