using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cuttingFabric.lib;

namespace cuttingFabric
{
    class Program
    {
        private static string[] _listOfFabricClaims = File.ReadAllLines(@"data\fabricClaimData.txt");

        static void Main(string[] args)
        {
            CalculateTotalOverlapingSquareInches();
            FindClaimThatDoesNotOverlap();
        }

        private static void FindClaimThatDoesNotOverlap()
        {
            List<Claim> overlapingClaims = new List<Claim>();
            List<Claim> claims = new List<Claim>();

            var f = new cuttingFabric.lib.Fabric(1000);
            foreach (var inputString in _listOfFabricClaims)
            {
                var c = new cuttingFabric.lib.Claim(inputString);
                claims.Add(c);
                f.AddClaim(c);
            }

            foreach (var c in claims)
            {
                if (f.DoesItOverlap(c))
                {
                    // do nothing
                } else
                {
                    overlapingClaims.Add(c);
                }
            }

            Console.WriteLine($"Of the {claims.Count} claims, only...");
            Console.WriteLine($"Found [{overlapingClaims.Count}] claims that overlap!");
            foreach (var c in overlapingClaims)
            {
                Console.WriteLine($"Claim ID:{c.id} overlaps.");
            }
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void CalculateTotalOverlapingSquareInches()
        {
            var f = new cuttingFabric.lib.Fabric(1000);
            foreach (var inputString in _listOfFabricClaims)
            {
                var c = new cuttingFabric.lib.Claim(inputString);
                f.AddClaim(c);
            }
            Console.WriteLine($"There are [{f.CalculateOverlapingSquareInches()}] sq.in. with overlaping fabric claims.");
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
