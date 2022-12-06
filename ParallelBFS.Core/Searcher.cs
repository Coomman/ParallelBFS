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

            for (int i = 0; i < cur.Count; i++)
            {
                var node = cur[i];

                if (node.NotVisited)
                {
                    stack.Push(node);
                }
            }
        }
    }
    
    public static void Bfs(this Graph graph, int start)
    {
        var currentLayer = new Queue<Node>();
        var nextLayer = new Queue<Node>();
        
        graph[start].Depth = 0;
        currentLayer.Enqueue(graph[start]);

        int depth = 1;
        while (currentLayer.Any())
        {
            foreach (var node in currentLayer)
            {
                ProcessNode(node, depth, nextLayer);
            }

            currentLayer = nextLayer;
            nextLayer = new Queue<Node>();

            depth++;
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
    
    private static void ProcessNode(Node cur, int depth, Queue<Node> nextLayer)
    {
        for (int i = 0; i < cur.Count; i++)
        {
            var node = cur[i];

            if (node.NotVisited)
            {
                node.Depth = depth;
                nextLayer.Enqueue(node);
            }
        }
    }
    
    private static void ProcessNode(Node cur, int depth, ConcurrentQueue<Node> nextLayer)
    {
        for (int i = 0; i < cur.Count; i++)
        {
            var node = cur[i];

            if (Interlocked.CompareExchange(ref node.Depth, depth, -1) == -1)
            {
                nextLayer.Enqueue(node);
            }
        }
    }
}