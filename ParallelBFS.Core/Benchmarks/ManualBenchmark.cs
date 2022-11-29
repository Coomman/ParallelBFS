using System.Diagnostics;

namespace ParallelBFS.Core.Benchmarks;

public static class ManualBenchmark
{
    public static void CompareRun(int runsCount)
    {
        long seq = 0, par = 0;
        for (int i = 0; i < runsCount; i++)
        {
            Console.WriteLine($"Iteration #{i + 1}");

            var graph = GraphGenerator.CreatedConnectedGraph(1000, 5000);

            var sw = Stopwatch.StartNew();

            graph.Bfs(0);

            seq += sw.ElapsedMilliseconds;
            
            graph.Reset();

            sw = Stopwatch.StartNew();

            graph.ParallelBfs(0);

            par += sw.ElapsedMilliseconds;
    
            sw.Reset();
        }

        double seqAvg = seq / (double) runsCount, parAvg = par / (double) runsCount;

        Console.WriteLine();
        Console.WriteLine("Sequential elapsed: " + TimeSpan.FromMilliseconds(seqAvg));
        Console.WriteLine("Parallel elapsed: " + TimeSpan.FromMilliseconds(parAvg));
        Console.WriteLine($"Parallel is {(seqAvg / parAvg - 1) * 100:F}% faster");
    }
}