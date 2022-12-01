using System.Collections;

namespace ParallelBFS.Core.Models;

public class Graph : IEnumerable<Node>
{
    private readonly IList<Node> _nodes;

    public Node this[int index] => _nodes[index];
        
    public Graph(int[][] edges)
    {
        _nodes = Enumerable.Repeat(0, edges.Length)
            .Select((_, i) => new Node(i.ToString(), new List<Node>()))
            .ToArray();

        for (int i = 0; i < edges.Length; i++)
        {
            foreach (var edge in edges[i])
            {
                _nodes[i].AddEdge(_nodes[edge]);
            }
        }
    }

    public Graph(IList<Node> nodes)
    {
        _nodes = nodes;
    }

    public int NodesCount => _nodes.Count;

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