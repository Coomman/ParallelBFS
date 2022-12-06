using FluentAssertions;
using ParallelBFS.Core;
using ParallelBFS.Core.Generators;

namespace ParallelBFS.Tests;

public class GraphGeneratorTests
{
    [Fact]
    public void CreateConnectedGraph_ShouldCreateConnectedGraph()
    {
        var graph = GraphGenerator.CreateConnectedGraph(1000, 100000);
        
        graph.Dfs(0);

        for (int i = 0; i < graph.Count; i++)
        {
            graph[i].NotVisited.Should().BeFalse();
        }
    }

    [Fact]
    public void CreateCube_ShouldCreateCorrectCube()
    {
        for (int n = 2; n < 50; n++)
        {
            var graph = GraphGenerator.CreateCube(n);
            graph.Bfs(0);

            var distances = new Dictionary<int, int>();
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    for (int k = 0; k <= n; k++)
                    {
                        var dist = i + j + k;

                        distances.TryGetValue(dist, out var count);
                        distances[dist] = count + 1;
                    }
                }
            }

            foreach (var (dist, count) in distances)
            {
                var realCount = 0;
                for (int i = 0; i < graph.Count; i++)
                {
                    if (graph[i].Depth == dist)
                    {
                        realCount++;
                    }
                }
                
                realCount.Should().Be(count);
            }
        }
    }
}