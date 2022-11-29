using System.Collections;

namespace ParallelBFS.Core.Models;

public class Node : IEnumerable<Node>
{
    private readonly IList<Node> _edges;
    
    public int Index { get; }

    public int Depth = -1;

    public Node(int index, IList<Node> edges)
    {
        Index = index;
        _edges = edges;
    }

    public bool NotVisited => Depth == -1;

    public void AddEdge(Node to)
    {
        _edges.Add(to);
    }

    public bool IsConnected(Node to)
    {
        return _edges.Any(x => x == to);
    }
    
    public IEnumerator<Node> GetEnumerator()
    {
        return _edges.GetEnumerator();
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"Index = {Index} | Depth = {Depth} | Neighbors = {{ {string.Join(", ", _edges.Select(x => x.Index))} }}";
    }
}