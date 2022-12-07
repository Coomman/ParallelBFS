# Parallel BFS Implementation

Repository contains both sequential and parallel versions of BFS algorithm and benchmarks to test the performance.

## [Sequential version](https://github.com/Coomman/ParallelBFS/blob/2edd3db6aa3f6a2edf20efb706b97a15e28ba018/ParallelBFS.Core/Searcher.cs#L32)
Basic BFS algorithm using standard Queue.

## [Parallel version](https://github.com/Coomman/ParallelBFS/blob/2edd3db6aa3f6a2edf20efb706b97a15e28ba018/ParallelBFS.Core/Searcher.cs#L55)
This version of the algorithm is using *layer by layer* approach.  
For layer container, ConcurrentQueue is being used (that is basically Fetch-and-Add Queue):
1. Create two empty layers (*Current* and *Next*) and enqueue starting node to the *Current* with ***depth*** = 0.
2. While the *Current* layer is not empty do on each node in the *Current* layer in parallel:
   1. Iterate on neighbors of the node.
   2. With ***CAS*** try to change the ***depth*** of the neighbor to the ***depth*** of node + 1.
   3. If ***CAS*** succeeds add the neighbor to the *Next* layer.
3. Change the *Current* layer on the *Next* and create a new empty *Next* layer.

# Benchmarks
**Task:** BFS on 500x500x500 cube from {0; 0; 0} point using a maximum of 4 threads.  
**Used benchmark:** BenchmarkDotNet 15 runs average.

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19045.2251)  
Intel Core i9-9900K CPU 5.00GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores  
.NET SDK=7.0.100  
[Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2  
Job-EKYYXI : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2

| Method         | Cube Size   | Mean     | Error    | StdDev   |
|----------------|-------------|----------|----------|----------|
| Sequential BFS | 500x500x500 | 36.387 s | 0.1466 s | 0.0483 s |
| Parallel BFS   | 500x500x500 | 11.702 s | 0.0573 s | 0.0118 s |

The **Parallel** BFS is approximately **211%** faster than the **Sequential** version.
