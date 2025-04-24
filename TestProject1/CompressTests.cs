using System.Diagnostics;
using Xunit.Abstractions;

namespace Common.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CompressBigRequest()
        {
            var data = await File.ReadAllTextAsync("TestData/large_data.json");

            _testOutputHelper.WriteLine($"Исходный размер: {data.Length * 2}");
            var sw = Stopwatch.StartNew();

            var compressed = data.Compress();
            var decompressed = compressed.Decompress();
            sw.Stop();

            _testOutputHelper.WriteLine($"Сжатый размер: {compressed.Length}");
            _testOutputHelper.WriteLine($"Потрачено времени: {sw.Elapsed}");
        }
    }
}
