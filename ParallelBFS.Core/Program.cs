using BenchmarkDotNet.Running;
using ParallelBFS.Core.Benchmarks;

//ManualBenchmark.CompareRun(5);

BenchmarkRunner.Run<SearcherBenchmark>();