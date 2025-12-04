using AOC.Common;
using System.Diagnostics;

namespace AOC.Day03
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            List<string> batteryBanks = LoadInput(Console.In);

            var ts1 = Stopwatch.GetTimestamp();
            var puzzle1 = PuzzleRefactor.Calc(batteryBanks, 2);
            Console.WriteLine($"Puzzle 1: {puzzle1}");

            var puzzle2 = PuzzleRefactor.Calc(batteryBanks, 12);
            Console.WriteLine($"Puzzle 2: {puzzle2}");

            var ts2 = Stopwatch.GetTimestamp();
            Console.WriteLine("Elapsed time: " +
                $"{(ts2 - ts1) * 1000 / (double)Stopwatch.Frequency:N0} ms");
        }

        private static List<string> LoadInput(this TextReader text) =>
            text.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
