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
        var arr1 = new int[graph.Count];
        for (int i = 0; i < arr1.Length; i++)
        {
            arr1[i] = graph[i].Depth;
        }

        graph.Reset();
        graph.ParallelBfs(0);
        
        var arr2 = new int[graph.Count];
        for (int i = 0; i < arr2.Length; i++)
        {
            arr2[i] = graph[i].Depth;
        }

        arr1.SequenceEqual(arr2).Should().BeTrue();
    }
}