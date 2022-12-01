using System.Collections;
using System.Diagnostics;

namespace ParallelBFS.Core.Models;

public class Node : IEnumerable<Node>
{
    private IList<Node> _edges = ArraySegment<Node>.Empty;
    
    public string? Name { get; }

    public int Depth = -1;

    public Node(string name, IList<Node> edges)
    {
        Name = name;
        _edges = edges;
    }

    public Node()
    { }

    public bool NotVisited => Depth == -1;

    public void SetEdges(IList<Node> edges)
    {
        _edges = edges;
    }

    public void AddEdge(Node to)
    {
        _edges.Add(to);
    }

    public void RemoveEdge(int index, string nodeName)
    {
        Debug.Assert(_edges[index].Name == nodeName);
        _edges.RemoveAt(index);
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
        return $"Name = {Name} | Depth = {Depth} | Neighbors = {{ {string.Join(", ", _edges.Select(x => x.Name))} }}";
    }
}