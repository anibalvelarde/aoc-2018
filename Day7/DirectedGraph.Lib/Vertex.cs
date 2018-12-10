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

        public bool HasDependencies()
        {
            return !_dependencies.Count.Equals(0);
        }

        public bool IsNeededByOthers()
        {
            return !_neededBy.Count.Equals(0);
        }

        internal void NeededBy(Vertex v)
        {
            _neededBy.Add(v.Id, v);
        }

        internal List<char> GetPrecedenceSequence(List<char> initialSequence)
        {
            var origins = GetOriginPoints();
            List<char> seq = origins.Aggregate(initialSequence, (acc, v) =>
            {
                return v.GetPrecedenceSequence(acc);
            });
            return AddSelf(seq);
        }

        private List<char> AddSelf(List<char> seq)
        {
            if (!seq.Contains(this.Id))
            {
                seq.Insert(0, this.Id);
            }
            return seq;
        }

        private List<Vertex> GetOriginPoints()
        {
            return _neededBy
                    .Where(v => v.Value.DependsOn(this))
                    .Select(dependency => dependency.Value)
                    .OrderByDescending(x => x.Id)
                    .ToList();
        }
    }
}
