using BenchmarkDotNet.Running;

namespace Patterns.Util.Mapping.BenchmarkTests {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run(typeof(MapperBenchmarks));
        }
    }
}
