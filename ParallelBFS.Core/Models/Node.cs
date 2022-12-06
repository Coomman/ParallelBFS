namespace ParallelBFS.Core.Models;

public class Node
{
    private Node[] _edges = Array.Empty<Node>();

    public volatile int Depth = -1;

    public bool NotVisited => Depth == -1;

    public int Count => _edges.Length;

    public void SetEdges(Node[] edges)
    {
        _edges = edges;
    }
    
    public Node this[int index] => _edges[index];

    public void AddEdge(Node to)
    {
        throw new InvalidOperationException(
            "Change neighbours type to IList<Node> and uncomment code below in order to use");

        // if (_edges is Node[])
        // {
        //     _edges = new List<Node>();
        // }
        //
        // _edges.Add(to);
    }

    public bool IsConnected(Node to)
    {
        return _edges.Any(x => x == to);
    }
}