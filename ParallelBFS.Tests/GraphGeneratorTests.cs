using FluentAssertions;
using ParallelBFS.Core;
using ParallelBFS.Core.Generators;
using ParallelBFS.Core.Models;

namespace ParallelBFS.Tests;

public class GraphGeneratorTests
{
    [Fact]
    public void CreateConnectedGraph_ShouldCreateConnectedGraph()
    {
        var graph = GraphGenerator.CreateConnectedGraph(1000, 100000);
        
        graph.Dfs(0);

        graph.Should().AllSatisfy(x => x.Depth.Should().Be(1));
    }

    [Fact]
    public void CreateCube_ShouldCreateCorrectCube()
    {
        for (int i = 2; i < 10; i++)
        {
            var graph = GraphGenerator.CreateCube(i);
            graph.Bfs(0);

            graph.Should().AllSatisfy(x => x.Depth.Should().Be(GetDistance(x)), i.ToString());
        }
    }

    private static int GetDistance(Node node)
    {
        return node.Name.Trim('{', '}').Trim().Split(", ").Select(int.Parse).Sum();
    }
}