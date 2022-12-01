using ParallelBFS.Core.Models;

namespace ParallelBFS.Core.Generators;

public static class TestGenerator
{
    public static Graph CreateHollowCube(int n)
    {
        var array = Enumerable.Range(0, n + 1).ToArray();

        var nodes = new[]
        {
            CreateEdges(array, 0, 0, 0),
            CreateEdges(array, 0, n, n),
            CreateEdges(array, n, 0, n),
            CreateEdges(array, n, n, 0)
        };

        var graph = nodes.Select(DebugInfo).ToArray();

        LinkNodes(nodes[0][0][^1], nodes[2][2], 0);
        LinkNodes(nodes[0][0][^1], nodes[3][1], 0);
        
        graph = nodes.Select(DebugInfo).ToArray();

        LinkNodes(nodes[0][1][^1], nodes[1][2], 0);
        LinkNodes(nodes[0][1][^1], nodes[3][0], 0);
        
        graph = nodes.Select(DebugInfo).ToArray();

        LinkNodes(nodes[0][2][^1], nodes[1][1], 0);
        LinkNodes(nodes[0][2][^1], nodes[2][0], 0);
        
        graph = nodes.Select(DebugInfo).ToArray();
        
        LinkNodes(nodes[1][0][^1], nodes[2][1], nodes[2][1].Count - 1);
        LinkNodes(nodes[1][0][^1], nodes[3][2], nodes[3][2].Count - 1);
        
        graph = nodes.Select(DebugInfo).ToArray();

        return new Graph(nodes.SelectMany(g => g.SelectMany(x => x)).ToList());
    }

    private static Graph DebugInfo(List<Node>[] nodes)
    {
        return new Graph(nodes.SelectMany(x => x).OrderBy(x => x.Name).ToArray());
    }
    
    private static List<Node>[] CreateEdges(int[] array, int x, int y, int z)
    {
        var nodes = new[]
        {
            array.Select(i => new Node($"{{ {i}, {y}, {z} }}", new List<Node>())).ToList(),
            array.Select(i => new Node($"{{ {x}, {i}, {z} }}", new List<Node>())).ToList(),
            array.Select(i => new Node($"{{ {x}, {y}, {i} }}", new List<Node>())).ToList()
        };

        foreach (var edge in nodes)
        {
            for (int i = 1; i < edge.Count; i++)
            {
                edge[i].AddEdge(edge[i - 1]);
                edge[i - 1].AddEdge(edge[i]);
            }
        }

        var xNode = nodes[0][x == 0 ? 0 : nodes[0].Count - 1];
        LinkNodes(xNode, nodes[1], y);
        LinkNodes(xNode, nodes[2], z);

        return nodes;
    }

    private static void LinkNodes(Node xNode, List<Node> edge, int index)
    {
        edge.RemoveAt(index == 0 ? 0 : edge.Count - 1);

        var node = edge[index == 0 ? 0 : edge.Count - 1];
        node.RemoveEdge(index == 0 ? 0 : node.Count() - 1, xNode.Name);
        
        xNode.AddEdge(node);
        node.AddEdge(xNode);
    }
}