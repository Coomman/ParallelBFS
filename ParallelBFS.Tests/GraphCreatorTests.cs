using FluentAssertions;
using ParallelBFS.Core;

namespace ParallelBFS.Tests;

public class GraphCreatorTests
{
    [Fact]
    public void CreatedConnectedGraph_ShouldCreateConnectedGraph()
    {
        var graph = GraphGenerator.CreatedConnectedGraph(1000, 100000);
        
        graph.Dfs(0);

        graph.Should().AllSatisfy(x => x.Depth.Should().Be(1));
    }
}