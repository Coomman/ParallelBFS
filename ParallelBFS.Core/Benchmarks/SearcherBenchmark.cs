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