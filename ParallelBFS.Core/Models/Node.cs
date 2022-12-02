using System.Collections;

namespace ParallelBFS.Core.Models;

public class Node : IEnumerable<Node>
{
    private IList<Node> _edges = Array.Empty<Node>();

    public int Depth = -1;

    public bool NotVisited => Depth == -1;

    public void SetEdges(Node[] edges)
    {
        _edges = edges;
    }

    public void AddEdge(Node to)
    {
        if (_edges is Node[])
        {
            _edges = new List<Node>();
        }
        
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
        return $"Size: {_edges.Count}";
    }
}