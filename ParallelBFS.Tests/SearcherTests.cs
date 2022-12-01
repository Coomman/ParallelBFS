using FluentAssertions;
using ParallelBFS.Core;
using ParallelBFS.Core.Generators;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Tests;

public class SearcherTests
{
    [Fact]
    public void ParallelBfs_RandomGraph_Correctness()
    {
        var graph = GraphGenerator.CreateConnectedGraph(1000, 100000);
        
        TestCorrectness(graph);
    }

    [Fact]
    public void ParallelBfs_Cube_Correctness()
    {
        var graph = GraphGenerator.CreateCube(100);
        
        TestCorrectness(graph);
    }

    private static void TestCorrectness(Graph graph)
    {
        graph.Bfs(0);
        var res = graph.Select(x => x.Depth).ToArray();
        
        graph.Reset();
        graph.ParallelBfs(0);

        graph.Select(x => x.Depth).SequenceEqual(res).Should().BeTrue();
    }
}