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
        public SortedList<char, Vertex> AvailableSteps = new SortedList<char, Vertex>();
        private string _orderOfSteps = "";

        public void AddStatement(string s)
        {
            var pattern = MakePattern(s);
            var v1 = pattern.Groups["vortex1"].Value.ToCharArray()[0];
            var v2 = pattern.Groups["vortex2"].Value.ToCharArray()[0];

            if (!Vertices.ContainsKey(v1)) Vertices.Add(v1, new Vertex(v1));
            if (!Vertices.ContainsKey(v2)) Vertices.Add(v2, new Vertex(v2));

            Vertices[v2].AddDependency(Vertices[v1]);
            Vertices[v1].AddNeededBy(Vertices[v2]);
        }

        public string GetPrecedenceSequence()
        {
            LoadStartingSteps();
            while (AvailableSteps.Count > 0)
            {
                var nextStep = GetNextStep();
                if (IsIncludedInOrderOfSteps(nextStep))
                {
                    _orderOfSteps += nextStep.Id.ToString();
                    Console.WriteLine(_orderOfSteps);
                }
                foreach (var step in nextStep.NeededBy)
                {
                    if (IsIncludedInOrderOfSteps(step.Value) && 
                        !AvailableSteps.ContainsKey(step.Key))
                    {
                        AvailableSteps.Add(step.Key, step.Value);
                    }
                }
            }
            return _orderOfSteps;
        }

        private bool IsIncludedInOrderOfSteps(Vertex nextStep)
        {
            return !_orderOfSteps.Contains(nextStep.Id);
        }

        private Vertex GetNextStep()
        {
            Vertex nextStep = AvailableSteps.First().Value;
            foreach (var nextStepCandidate in AvailableSteps)
            {
                if (ReadyToAssembleStep(nextStepCandidate.Value))
                {
                    nextStep = nextStepCandidate.Value;
                    AvailableSteps.Remove(nextStep.Id);
                    break;
                }
            }
            return nextStep;
        }

        private bool ReadyToAssembleStep(Vertex v)
        {
            bool stepIsReady = true;
            foreach (var dep in v.DependencyList)
            {
                if (!_orderOfSteps.Contains(dep.Key))
                {
                    stepIsReady = false;
                    break;
                }
            }
            return stepIsReady;
        }

        private void LoadStartingSteps()
        {
            var startingSteps = Vertices
                    .Where(v => !v.Value.HasDependencies)
                    .Select(noDepKeyValPair => noDepKeyValPair.Value)
                    .OrderBy(i => i.Id)
                    .ToList();
            foreach (var s in startingSteps)
            {
                if (AvailableSteps.ContainsKey(s.Id))
                {
                    // step is already in the list - do nothing
                } else
                {
                    AvailableSteps.Add(s.Id, s);
                }
            }
        }

        private List<Vertex> GetLeafNodes()
        {
            var listOfLeafNodes = Vertices
                    .Where(v => !v.Value.IsNeededByAnyStep)
                    .Select(leafNodeKvp => leafNodeKvp.Value)
                    .OrderBy(i => i.Id)
                    .ToList();

            foreach (var l in listOfLeafNodes)
            {
                Console.WriteLine($"Leaf node: [{l.Id}].");
            }

            return listOfLeafNodes;
        }

        private Match MakePattern(string input)
        {
            Regex pattern = new Regex(@"Step (?<vortex1>\D) must be finished before step (?<vortex2>\D) can begin.");
            return pattern.Match(input);
        }
    }
}
