using FluentAssertions;
using ParallelBFS.Core;

namespace ParallelBFS.Tests;

public class SearcherTests
{
    [Fact]
    public void ParallelBfs_Correctness()
    {
        var graph = GraphGenerator.CreatedConnectedGraph(1000, 100000);
        
        graph.Bfs(0);
        var res = graph.Select(x => x.Depth).ToArray();
        
        graph.Reset();
        graph.ParallelBfs(0);

        graph.Select(x => x.Depth).SequenceEqual(res).Should().BeTrue();
    }
}