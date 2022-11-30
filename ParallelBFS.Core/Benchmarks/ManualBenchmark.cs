using System.Diagnostics;

namespace ParallelBFS.Core.Benchmarks;

public static class ManualBenchmark
{
    public static void CompareRun(int runsCount)
    {
        var graph = GraphGenerator.CreatedConnectedGraph(100, 1000);
        
        long seq = 0, par = 0;
        for (int i = 0; i < runsCount; i++)
        {
            Console.WriteLine($"Iteration #{i + 1}");

            var sw = Stopwatch.StartNew();

            graph.Bfs(0);

            seq += sw.ElapsedTicks;
            
            graph.Reset();

            sw = Stopwatch.StartNew();

            graph.ParallelBfs(0);

            par += sw.ElapsedTicks;
    
            sw.Reset();
        }

        long seqAvg = seq / runsCount, parAvg = par / runsCount;

        Console.WriteLine();
        Console.WriteLine("Sequential elapsed: " + TimeSpan.FromTicks(seqAvg));
        Console.WriteLine("Parallel elapsed: " + TimeSpan.FromTicks(parAvg));
        Console.WriteLine($"Parallel is {(seqAvg / (double) parAvg - 1) * 100:F}% faster");
    }
}