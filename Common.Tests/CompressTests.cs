using System.Diagnostics;

namespace Common.Tests
{
    public class Tests
    {
        [Test]
        public async Task CompressBigRequest()
        {
            var data = await File.ReadAllTextAsync("TestData/large_data.json");

            Console.WriteLine($"Исходный размер: {data.Length * 2}");
            var sw = Stopwatch.StartNew();

            var compressed = data.Compress();
            var decompressed = compressed.Decompress();
            sw.Stop();

            Console.WriteLine($"Сжатый размер: {compressed.Length}");
            Console.WriteLine($"Потрачено времени: {sw.Elapsed}");
        }
    }
}
