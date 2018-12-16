using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Lib
{

    public class Node
    {
        private List<Node> _children;

        public Node(Header h, Meta m)
        {
            Header = h;
            Meta = m;
        }

        public Meta Meta { get; }

        public Header Header { get; }

        public void BuildMyChildren(Node lastChildAdded, int[] input)
        {
            _children.Add(lastChildAdded);
            while (_children.Count != Header.ChildrenCount)
            {
                var nextChildIdx = (lastChildAdded.Meta.Index + lastChildAdded.Meta.InfoLength);
                var nextChild = BuildNextChild(nextChildIdx, input);
                _children.Add(nextChild);
                lastChildAdded = nextChild;
            }
        }

        private Node BuildNextChild(int index, int[] input)
        {
            //if (())
            //{
            //    var child = new Node(); 
            //}
            return null;
        }

        public Node BuildMyParent(int[] input)
        {
            if ((Meta.Index-2) >= 0)
            {
                var parentIdx = Meta.Index - 2; var childCount = input[parentIdx]; var metaCount = input[parentIdx + 1];
                var m = new Meta(parentIdx, childCount, metaCount, null);
                var h = new Header(childCount, metaCount);
                var n = new Node(h, m);
                n.BuildMyChildren(this, input);
                return n; 
            } else
            {
                throw new IndexOutOfRangeException("Tried to build a parent outside of the range of input data.");
            }
        }

        public void AddChild(Node child)
        {
            if (!_children.Contains(child))
            {
                this._children.Add(child); 
            }
        }
    }
}
