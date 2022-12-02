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
            Console.Write($"Iteration #{i + 1} completed in ");
            sw = Stopwatch.StartNew();

            var localSw = Stopwatch.StartNew();
            graph.Bfs(0);
            seq += localSw.ElapsedMilliseconds;
            graph.Reset();
            
            localSw = Stopwatch.StartNew();
            graph.ParallelBfs(0);
            par += localSw.ElapsedMilliseconds;
            graph.Reset();

            localSw.Reset();
            Console.WriteLine(sw.Elapsed);
        }
        sw.Reset();

        long seqAvg = seq / runsCount, parAvg = par / runsCount;

        Console.WriteLine();
        Console.WriteLine("Sequential elapsed: " + TimeSpan.FromMilliseconds(seqAvg));
        Console.WriteLine("Parallel elapsed: " + TimeSpan.FromMilliseconds(parAvg));
        Console.WriteLine($"Parallel is {(seqAvg / (double) parAvg - 1) * 100:F}% faster");
    }
}