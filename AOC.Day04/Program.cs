using AOC.Common;
using Microsoft.CSharp.RuntimeBinder;
using System.Diagnostics;

namespace AOC.Day04
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            List<string> matrix = Console.In.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            //List<string> matrix =
            //[
            //    "..@@.@@@@.",
            //    "@@@.@.@.@@",
            //    "@@@@@.@.@@",
            //    "@.@@@@..@.",
            //    "@@.@@@@.@@",
            //    ".@@@@@@@.@",
            //    ".@.@.@.@@@",
            //    "@.@@@.@@@@",
            //    ".@@@@@@@@.",
            //    "@.@.@@@.@.",
            //];

            var s1 = Stopwatch.GetTimestamp();
            var totalRollsPuzzle1 = Puzzle1.Calc(matrix);
            var s2 = Stopwatch.GetTimestamp();

            var elapsedMs = (s2 - s1) * 1000 / Stopwatch.Frequency;

            Console.WriteLine($"Total rolls: {totalRollsPuzzle1} in {elapsedMs}ms");

            var s3 = Stopwatch.GetTimestamp();
            var totalRollsPuzzle2 = Puzzle2.Calc(matrix);
            var s4 = Stopwatch.GetTimestamp();

            var elapsedMsPuzzle2 = (s4 - s3) * 1000 / Stopwatch.Frequency;

            var s5 = Stopwatch.GetTimestamp();
            var totalRollsPuzzle2fast = Puzzle2Fast.Calc(matrix);
            var s6 = Stopwatch.GetTimestamp();

            var elapsedMsPuzzle2fast = (s6 - s5) * 1000 / Stopwatch.Frequency;

            Console.WriteLine($"Total rolls: {totalRollsPuzzle2fast} in {elapsedMsPuzzle2fast}ms");
        }
    }
}
