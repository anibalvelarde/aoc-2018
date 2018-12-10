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

        public void AddDependency(Vertex v)
        {
            _dependencies.Add(v.Id, v);
        }

        public void AddNeededBy(Vertex v)
        {
            _neededBy.Add(v.Id, v);
        }

        public SortedList<char,Vertex> DependencyList
        {
            get
            {
                return _dependencies;
            }
        }

        public bool HasDependencies
        {
            get
            {
                return _dependencies.Count > 0;
            }
        }

        public SortedList<char, Vertex> NeededBy
        {
            get
            {
                return _neededBy;
            }
        }

        public bool IsNeededByAnyStep
        {
            get
            {
                return _neededBy.Count > 0;
            }
        }
    }
}
