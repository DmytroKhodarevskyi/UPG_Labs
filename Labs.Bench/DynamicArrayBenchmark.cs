using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Labs;
using static System.Runtime.InteropServices.JavaScript.JSType;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

[MemoryDiagnoser]
[ShortRunJob]
public class DynamicArrayBenchmark
{
    private DynamicArray<int> array;

    [Params(100_000)]
    public int Size;

    [IterationSetup]
    public void Setup()
    {
        array = new DynamicArray<int>();
    }

    [Benchmark]
    public void AddBenchmark()
    {
        for (int i = 0; i < Size; i++)
        {
            array.Add(i);
        }
    }

    [Benchmark]
    public void InsertBenchmark()
    {
        for (int i = 0; i < Size; i++)
        {
            array.Insert(0, i);
        }
    }
}
