using System.Collections.Concurrent;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Core;

public static class Searcher
{
    private const int Parallelism = 4;

    public static void Dfs(this Graph graph, int start)
    {
        var stack = new Stack<Node>();
        stack.Push(graph[start]);

        while (stack.Any())
        {
            var cur = stack.Pop();
            cur.Depth = 1;

            foreach (var node in cur)
            {
                if (node.NotVisited)
                {
                    stack.Push(node);
                }
            }
        }
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
        var currentLayer = new ConcurrentQueue<Node>();
        var nextLayer = new ConcurrentQueue<Node>();
        
        graph[start].Depth = 0;
        currentLayer.Enqueue(graph[start]);

        int depth = 1;
        while (currentLayer.Any())
        {
            currentLayer
                .AsParallel()
                .WithDegreeOfParallelism(Parallelism)
                .ForAll(node => ProcessNode(node, depth, nextLayer));

            currentLayer = nextLayer;
            nextLayer = new ConcurrentQueue<Node>();

            depth++;
        }
    }
    
    private static void ProcessNode(Node cur, int depth, ConcurrentQueue<Node> nextLayer)
    {
        foreach (var node in cur)
        {
            if (Interlocked.CompareExchange(ref node.Depth, depth, -1) == -1)
            {
                nextLayer.Enqueue(node);
            }
        }
    }
}