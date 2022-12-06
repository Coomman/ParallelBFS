using BenchmarkDotNet.Running;
using ParallelBFS.Core.Benchmarks;

//ManualBenchmark.CompareRun(500, 5);

BenchmarkRunner.Run<SearcherBenchmark>();