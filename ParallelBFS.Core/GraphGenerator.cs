using ParallelBFS.Core.Models;

namespace ParallelBFS.Core;

public static class GraphGenerator
{
    public static Graph CreatedConnectedGraph(int nodes, int edges)
    {
        var nodesList = Enumerable.Repeat(0, nodes)
            .Select((_, i) => new Node(i, new List<Node>()))
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