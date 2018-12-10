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
        public List<Worker> _workers = new List<Worker>();
        private string _orderOfSteps = "";
        private int _ticks = 0;

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
            SetupWorkers(2);
            while (AvailableSteps.Count > 0)
            {
                var nextStep = GetNextStep();
                AssignWork(nextStep);
                if (IsIncludedInOrderOfSteps(nextStep))
                {
                    if (nextStep.IsDone)
                    {
                        _orderOfSteps += nextStep.Id.ToString();
                        Console.WriteLine(_orderOfSteps);
                    }
                }
                foreach (var step in nextStep.NeededBy)
                {
                    if (IsIncludedInOrderOfSteps(step.Value) && 
                        !AvailableSteps.ContainsKey(step.Key))
                    {
                        AvailableSteps.Add(step.Key, step.Value);
                    }
                }
                IncreaseByOneTick();
            }
            return _orderOfSteps;
        }

        private void AssignWork(Vertex nextStep)
        {
            if (ReadyToAssembleStep(nextStep))
            {
                var w = GetNextIdleWorker();
                if (w is null)
                {
                    // do nothing
                }
                else
                {
                    if (!nextStep.DidWorkStart)
                    {
                        nextStep.DidWorkStart = true;
                        w.AssignTask(nextStep);
                    }
                } 
            }
        }

        private void SetupWorkers(int workerCount, int stepDuration = 1)
        {
            for (int i = 0; i < workerCount; i++)
            {
                _workers.Add(new Worker(stepDuration));
            }
        }

        private void IncreaseByOneTick()
        {
            _ticks++;
            foreach (var w in _workers)
            {
                w.Tick();
            }
        }

        private Worker GetNextIdleWorker()
        {
            foreach (var w in _workers)
            {
                if (w.NotBusy()) return w;
            }
            return null;
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
                    if (nextStep.IsDone)
                    {
                        AvailableSteps.Remove(nextStep.Id);
                        break;
                    }                }
            }
            return nextStep;
        }

        private bool ReadyToAssembleStep(Vertex v)
        {
            if (v.DidWorkStart && !v.IsDone) return false;
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

        private Match MakePattern(string input)
        {
            Regex pattern = new Regex(@"Step (?<vortex1>\D) must be finished before step (?<vortex2>\D) can begin.");
            return pattern.Match(input);
        }
    }
}
