using System;
using System.Collections;
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
        public SortedList<char, Vertex> ToDo = new SortedList<char, Vertex>();
        public SortedList<char, Vertex> Doing = new SortedList<char, Vertex>();
        public List<Vertex> Done = new List<Vertex>();
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

        public string GetPrecedenceSequence(int workerCount = 2, int timeDialationOffset = 0)
        {
            Initialize(workerCount, timeDialationOffset);
            while (ToDo.Count > 0 || Doing.Count > 0)
            {
                if (Done.Count.Equals(26))
                {
                    WorkerReport();
                    Console.ReadLine();
                }

                var workers = GetIdleWorkers();
                List<Vertex> tasks = GetReadyTasks();
                AssignWork(workers, tasks);
                NextTimeSlice(workers);
            }
            WorkerReport();
            var result = new string(Done.Select(x => x.Id).ToArray());
            Console.WriteLine($"Result:  {result}");
            return result;
        }

        private void WorkerReport()
        {
            Console.WriteLine("------------------------------------------");
            foreach (var w in _workers)
            {
                Console.WriteLine($"Worker: {w.GetHashCode()}  [{w.ToString()}]");
            }
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($" Total Time: {this._ticks}");
            Console.WriteLine("------------------------------------------");
        }

        private List<Vertex> GetReadyTasks()
        {
            return ToDo
                    .Where(ReadyToAssembleStep)
                    .Select(i => i.Value)
                    .ToList();
        }

        private void Initialize(int workerCount, int timeOffset)
        {
            LoadStartingSteps();
            SetupWorkers(workerCount, timeOffset);
        }

        private void AssignWork(List<Worker> workers, List<Vertex> tasks)
        {
            foreach (var w in workers)
            {
                if (tasks.Count() > 0)
                {
                    var task = tasks.FirstOrDefault(x => !x.DidWorkStart);
                    if (!(task is null))
                    {
                        AssignWork(w, task);
                    }
                }
            }
        }

        private void AssignWork(Worker w, Vertex nextTask)
        {
            if (!nextTask.DidWorkStart)
            {
                AnalyzeScheduleTaskDependencies(nextTask);
                nextTask.DidWorkStart = true;
                w.AssignTask(nextTask);
                Doing.Add(nextTask.Id, nextTask);
                ToDo.Remove(nextTask.Id);
            }
        }

        private void AnalyzeScheduleTaskDependencies(Vertex nextTask)
        {
            foreach (var need in nextTask.NeededBy)
            {
                if (!ToDo.ContainsKey(need.Key))
                {
                    ToDo.Add(need.Value.Id, need.Value);
                }
            }
        }

        private bool AnalyzeIsTaskDone(Vertex nextTask)
        {
            bool isReallyDone = true;
            if (nextTask.IsDone)
            {
                foreach (var dep in nextTask.DependencyList)
                {
                    if (!Done.Contains(dep.Value))
                    {
                        isReallyDone = false;
                        break;
                    }
                }
            } else
            {
                isReallyDone = false;
            }
            return isReallyDone;
        }

        private void SetupWorkers(int workerCount, int stepDuration = 1)
        {
            for (int i = 0; i < workerCount; i++)
            {
                _workers.Add(new Worker(stepDuration));
            }
        }

        private void NextTimeSlice(List<Worker> workers)
        {
            NextTimeSlice();
            var doneTasks = new List<Vertex>();
            foreach (var task in Doing)
            {
                if (AnalyzeIsTaskDone(task.Value))
                {
                    Done.Add(task.Value);
                    doneTasks.Add(task.Value);
                }
            }
            foreach (var task in doneTasks)
            {
                Doing.Remove(task.Id);
            }
        }

        private void NextTimeSlice()
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

        private List<Worker> GetIdleWorkers()
        {
            return _workers.Select(i => i)
                .Where(i => i.NotBusy())
                .ToList();
        }

        private bool ReadyToAssembleStep(KeyValuePair<char,Vertex> item)
        {
            if (item.Value.DidWorkStart && !item.Value.IsDone) return false;
            bool stepIsReady = true;
            foreach (var dep in item.Value.DependencyList)
            {
                if (!Done.Contains(dep.Value))
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
                if (ToDo.ContainsKey(s.Id))
                {
                    // step is already in the list - do nothing
                } else
                {
                    ToDo.Add(s.Id, s);
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
