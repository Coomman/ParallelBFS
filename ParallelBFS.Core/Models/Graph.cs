using System.Collections;

namespace ParallelBFS.Core.Models;

public class Graph : IEnumerable<Node>
{
    private readonly IList<Node> _nodes;

    public Node this[int index] => _nodes[index];

    public Graph(IList<Node> nodes)
    {
        _nodes = nodes;
    }

    public void Reset()
    {
        foreach (var node in _nodes)
        {
            node.Depth = -1;
        }
    }
    
    public IEnumerator<Node> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}