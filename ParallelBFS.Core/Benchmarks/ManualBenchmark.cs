using System.Diagnostics;
using ParallelBFS.Core.Generators;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Core.Benchmarks;

public static class ManualBenchmark
{
    public static void CompareRun(int size, int runsCount)
    {
        var sw = Stopwatch.StartNew();
        var graph = GraphGenerator.CreateCube(size);
        Console.WriteLine($"Graph creation took {sw.Elapsed}\n\n");
        
        var parAvg = RunWithStopwatch("Parallel:", graph, x => x.ParallelBfs(0), runsCount);
        var seqAvg = RunWithStopwatch("Sequential:", graph, x => x.Bfs(0), runsCount);

        Console.WriteLine();
        Console.WriteLine("Sequential elapsed: " + TimeSpan.FromMilliseconds(seqAvg));
        Console.WriteLine("Parallel elapsed: " + TimeSpan.FromMilliseconds(parAvg));
        Console.WriteLine($"Parallel is {(seqAvg / (double) parAvg - 1) * 100:F}% faster");
    }

    private static long RunWithStopwatch(string name, Graph graph, Action<Graph> action, int count)
    {
        Console.WriteLine(name);

        long time = 0;
        for (int i = 0; i < count; i++)
        {
            GC.Collect();
            var sw = Stopwatch.StartNew();

            action(graph);

            time += sw.ElapsedMilliseconds;
            Console.WriteLine($"#{i + 1} iteration done in {sw.Elapsed}");
    
            graph.Reset();
        }

        Console.WriteLine();

        return (long)(time / (double) count);
    }
}