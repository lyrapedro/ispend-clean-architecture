using BenchmarkDotNet.Running;

namespace iSpend.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        new BenchmarkSwitcher(new [] { typeof(Benchmark) }).Run(args);
    }
}
