using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectedGraph.Lib
{
    public class Vertex
    {
        private SortedList<char, Vertex> _dependencies = new SortedList<char, Vertex>();
        private SortedList<char, Vertex> _neededBy = new SortedList<char, Vertex>();

        public Vertex(char id)
        {
            this.Id = id;
        }
        public char Id { get; private set; }
        public SortedList<char, Vertex> StepsNeeded { get; internal set; }

        public void AddDependency(Vertex v)
        {
            _dependencies.Add(v.Id, v);
        }

        public bool DependsOn(Vertex v)
        {
            return !_dependencies.FirstOrDefault(x => x.Equals(v)).Equals(null);
        }

        public bool HasNoDependencies()
        {
            return _dependencies.Count.Equals(0);
        }

        internal void NeededBy(Vertex v)
        {
            _neededBy.Add(v.Id, v);
        }

        internal Stack<char> GetPrecedenceSequence(Stack<char> initialSequence)
        {
            var origins = GetOriginPoints();
            Stack<char> seq = origins.Aggregate(initialSequence, (acc, v) =>
            {
                return v.GetPrecedenceSequence(acc);;
            });
            return AddSelf(seq);
        }

        private Stack<char> AddSelf(Stack<char> seq)
        {
            var rev = new Stack<char>();
            while (seq.Count>0)
            {
                rev.Push(seq.Pop());
            }
            if (!rev.Contains(this.Id)) rev.Push(this.Id);
            return rev;
        }

        private List<Vertex> GetOriginPoints()
        {
            return _neededBy
                    .Where(v => v.Value.DependsOn(this))
                    .Select(dependency => dependency.Value)
                    .ToList();
        }
    }
}
