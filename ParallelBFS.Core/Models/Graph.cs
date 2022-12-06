namespace ParallelBFS.Core.Models;

public class Graph
{
    private readonly Node[] _nodes;

    public Graph(Node[] nodes)
    {
        _nodes = nodes;
    }
    
    public Node this[int index] => _nodes[index];

    public int Count => _nodes.Length;

    public void Reset()
    {
        for (int i = 0; i < Count; i++)
        {
            _nodes[i].Depth = -1;
        }
    }
}