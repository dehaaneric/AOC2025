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
            // [
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

            List<string> source1 = [.. matrix];
            var totalRollsPuzzle1 = Measure("Total rolls (Puzzle1)", () => Puzzle1.Calc(source1));
            
            List<string> source2 = [.. matrix];
            var totalRollsPuzzle2 = Measure("Total rolls (Puzzle2)", () => Puzzle2.Calc(source2));
            
            List<string> source3 = [.. matrix];
            var totalRollsPuzzle2CoPilot = Measure("Total rolls (Puzzle2CoPilot)", () => Puzzle2.Calc(source3));

            char[][] grid = ConvertMatrixToCharArray([.. matrix]);
            var totalRollsPuzzle2Erik = Measure("Total rolls (Erik)", () => PuzzleErik.Calc(grid));

        }

        private static char[][] ConvertMatrixToCharArray(List<string> value)
        {
            var result = new char[value.Count][];
            for (int i = 0; i < value.Count; i++)
            {
                result[i] = value[i].ToCharArray();
            }
            return result;
        }

        private static T Measure<T>(string label, Func<T> func)
        {
            var start = Stopwatch.GetTimestamp();
            var result = func();
            var end = Stopwatch.GetTimestamp();

            var elapsedMs = (end - start) * 1000 / Stopwatch.Frequency;
            Console.WriteLine($"{label}: {result} in {elapsedMs}ms");

            return result;
        }
    }
}
