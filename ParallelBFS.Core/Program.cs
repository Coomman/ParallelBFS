using ParallelBFS.Core;
using ParallelBFS.Core.Benchmarks;
using ParallelBFS.Core.Models;

// var vertices = new[]
// {
//     new[] { 1, 2 },
//     new[] { 0, 2, 3 },
//     new[] { 0, 1, 4 },
//     new[] { 1, 5 },
//     new[] { 2 },
//     new[] { 3 }
// };
//
// var graph = new Graph(vertices);
// graph.Bfs(0);

// var graph = GraphGenerator.CreatedConnectedGraph(10, 10);
// graph.Bfs(0);
//Console.WriteLine(string.Join(" ", graph.Select(x => x.Depth)));

ManualBenchmark.CompareRun(5);