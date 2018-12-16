using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Lib
{
    internal class NodeFactory
    {
        public Node BuildLeafNode(int leafIndex, int[] input)
        {
            if (input[leafIndex] != 0) throw new ArgumentException("Leaf node cannot have children.");

            var ln = new Node(null, null);

            return ln;
        }
    }
}
