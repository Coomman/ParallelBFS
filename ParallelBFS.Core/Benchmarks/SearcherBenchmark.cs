using BenchmarkDotNet.Attributes;
using ParallelBFS.Core.Generators;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Core.Benchmarks;

public class SearcherBenchmark
{
    private Graph _graph = null!;
    
    [Params(500)]
    public int Size { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _graph = GraphGenerator.CreateCube(Size);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        _graph.Reset();
        GC.Collect();
    }
    
    [Benchmark]
    public void Bfs()
    {
        _graph.Bfs(0);
    }

    [Benchmark]
    public void ParallelBfs()
    {
        _graph.ParallelBfs(0);
    }
}