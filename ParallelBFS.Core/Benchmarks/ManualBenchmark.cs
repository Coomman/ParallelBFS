using System.Diagnostics;
using ParallelBFS.Core.Generators;

namespace ParallelBFS.Core.Benchmarks;

public static class ManualBenchmark
{
    public static void CompareRun(int runsCount)
    {
        var sw = Stopwatch.StartNew();
        var graph = GraphGenerator.CreateCube(500);
        Console.WriteLine($"Graph created in {sw.Elapsed}\n");
        
        long seq = 0, par = 0;
        for (int i = 0; i < runsCount; i++)
        {
            Console.WriteLine($"Iteration #{i + 1}");

            sw = Stopwatch.StartNew();
            graph.Bfs(0);
            seq += sw.ElapsedTicks;
            graph.Reset();
            
            sw = Stopwatch.StartNew();
            graph.ParallelBfs(0);
            par += sw.ElapsedTicks;
            graph.Reset();
        }
        sw.Reset();

        long seqAvg = seq / runsCount, parAvg = par / runsCount;

        Console.WriteLine();
        Console.WriteLine("Sequential elapsed: " + TimeSpan.FromTicks(seqAvg));
        Console.WriteLine("Parallel elapsed: " + TimeSpan.FromTicks(parAvg));
        Console.WriteLine($"Parallel is {(seqAvg / (double) parAvg - 1) * 100:F}% faster");
    }
}