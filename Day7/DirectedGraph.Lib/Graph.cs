using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirectedGraph.Lib
{

    public class Graph
    {
        public Dictionary<char,Vertex> Vertices = new Dictionary<char, Vertex>();

        public void AddStatement(string s)
        {
            var pattern = MakePattern(s);
            var v1 = pattern.Groups["vortex1"].Value.ToCharArray()[0];
            var v2 = pattern.Groups["vortex2"].Value.ToCharArray()[0];

            if (!Vertices.ContainsKey(v1)) Vertices.Add(v1, new Vertex(v1));
            if (!Vertices.ContainsKey(v2)) Vertices.Add(v2, new Vertex(v2));

            Vertices[v2].AddDependency(Vertices[v1]);
            Vertices[v1].NeededBy(Vertices[v2]);
        }

        public string GetPrecedenceSequence()
        {
            List<Vertex> origins = GetOriginPoints();
            string seq = origins.Aggregate("", (acc, v) =>
            {
                var sb = new StringBuilder(acc);
                var steps = v.GetPrecedenceSequence(new Stack<char>());
                while (steps.Count > 0)
                {
                    var s = steps.Pop();
                    sb.Append(s);
                }
                return sb.ToString();
            });

            return seq;
        }

        private string ReverseString(string polymer)
        {
            var a = polymer.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

        private List<Vertex> GetOriginPoints()
        {
            return Vertices
                    .Where(v => v.Value.HasNoDependencies())
                    .Select(noDepKeyValPair => noDepKeyValPair.Value)
                    .ToList();
        }

        private Match MakePattern(string input)
        {
            Regex pattern = new Regex(@"Step (?<vortex1>\D) must be finished before step (?<vortex2>\D) can begin.");
            return pattern.Match(input);
        }
    }
}
