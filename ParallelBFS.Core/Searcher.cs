using System.Collections.Concurrent;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Core;

public static class Searcher
{
    private const int Parallelism = 4;

    public static void Dfs(this Graph graph, int start)
    {
        
    }
    
    public static void Bfs(this Graph graph, int start)
    {
        var queue = new Queue<Node>();
        
        graph[start].Depth = 0;
        queue.Enqueue(graph[start]);

        while (queue.Any())
        {
            var cur = queue.Dequeue();

            foreach (var node in cur)
            {
                if (node.NotVisited || node.Depth > cur.Depth + 1)
                {
                    node.Depth = cur.Depth + 1;
                    queue.Enqueue(node);
                }
            }
        }
    }
    
    public static void ParallelBfs(this Graph graph, int start)
    {
        var currentLayer = new ConcurrentBag<Node>();
        var nextLayer = new ConcurrentBag<Node>();
        
        graph[start].Depth = 0;
        currentLayer.Add(graph[start]);

        int depth = 1;
        while (currentLayer.Any())
        {
            currentLayer
                .AsParallel()
                .WithDegreeOfParallelism(Parallelism)
                .ForAll(node => BfsRoutine(node, depth, nextLayer));

            currentLayer = nextLayer;
            nextLayer = new ConcurrentBag<Node>();

            depth++;
        }
    }
    
    private static void BfsRoutine(Node cur, int depth, ConcurrentBag<Node> nextLayer)
    {
        cur
            .AsParallel()
            .WithDegreeOfParallelism(Parallelism)
            .Where(node => node.NotVisited)
            .ForAll(node =>
            {
                if (Interlocked.CompareExchange(ref node.Depth, depth, -1) == -1)
                {
                    nextLayer.Add(node);
                }
            });
    }
}