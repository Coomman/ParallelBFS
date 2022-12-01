using System.Diagnostics;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Core.Generators;

public static class GraphGenerator
{
    public static Graph CreateCube(int n)
    {
        var array = Enumerable.Range(0, ++n).ToArray();
        
        var sw = Stopwatch.StartNew();
        var nodes = array
            .AsParallel()
            .Select(_ => array
                .Select(_ => array
                    .Select(_ => new Node())
                    .ToArray())
                .ToArray())
            .ToArray();
        Console.WriteLine($"Nodes generated in {sw.Elapsed}");
        
        sw = Stopwatch.StartNew();
        array
            .AsParallel()
            .ForAll(i => Link(i, n, nodes));
        Console.WriteLine($"Nodes linked in {sw.Elapsed}");
        
        sw = Stopwatch.StartNew();
        var graph = new Graph(nodes
            .AsParallel()
            .SelectMany(x => x.SelectMany(y => y))
            .ToArray());
        Console.WriteLine($"Graph built in {sw.Elapsed}");

        return graph;
    }
    private static void Link(int i, int n, Node[][][] nodes)
    {
        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < n; k++)
            {
                var edges = new List<Node>(6);

                if (i - 1 >= 0)
                    edges.Add(nodes[i - 1][j][k]);
                if (i + 1 < n)
                    edges.Add(nodes[i + 1][j][k]);
                if (j - 1 >= 0)
                    edges.Add(nodes[i][j - 1][k]);
                if (j + 1 < n)
                    edges.Add(nodes[i][j + 1][k]);
                if (k - 1 >= 0)
                    edges.Add(nodes[i][j][k - 1]);
                if (k + 1 < n)
                    edges.Add(nodes[i][j][k + 1]);
                
                nodes[i][j][k].SetEdges(edges.ToArray());
            }
        }
    }

    public static Graph CreateConnectedGraph(int nodes, int edges)
    {
        var nodesList = Enumerable.Repeat(0, nodes)
            .Select((_, i) => new Node(i.ToString(), new List<Node>()))
            .ToArray();

        ConnectNodes(new List<Node>(nodesList));
        AddRandomEdges(nodesList, nodes, edges);

        return new Graph(nodesList);
    }
    
    private static void ConnectNodes(IList<Node> nodesList)
    {
        var cur = nodesList.PickRandom();

        var visited = new HashSet<Node> { cur };
        while (nodesList.Any())
        {
            var neighbor = nodesList.PickRandom();

            if (visited.Add(neighbor))
            {
                cur.AddEdge(neighbor);
                neighbor.AddEdge(cur);
            }

            cur = neighbor;
        }
    }
    
    private static void AddRandomEdges(IList<Node> nodesList, int nodes, int edges)
    {
        for (int i = 0; i < edges - nodes; i++)
        {
            while (true)
            {
                var first = nodesList[Random.Shared.Next(nodesList.Count)];
                var second = nodesList[Random.Shared.Next(nodesList.Count)];
                
                if (first == second)
                    continue;
                if (first.IsConnected(second))
                    continue;

                first.AddEdge(second);
                second.AddEdge(first);

                break;
            }
        }
    }

    private static T PickRandom<T>(this IList<T> list)
    {
        var index = Random.Shared.Next(list.Count);
        var elem = list[index];
        list.RemoveAt(index);

        return elem;
    }
}