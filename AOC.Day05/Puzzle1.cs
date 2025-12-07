using AOC.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AOC.Day05
{
    internal static class Puzzle1
    {
        internal static void Calc(List<string> inputs)
        {
            var ts1 = Stopwatch.GetTimestamp();

            var length = inputs.Count;

            var ingredients = new List<ulong>(length);
            var freshRanges = new List<FreshRange>(length);

            foreach (var line in inputs)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.IndexOf('-') < 0)
                {
                    ingredients.Add(ulong.Parse(line));
                }
                else
                {
                    var parts = line.Split('-');
                    var start = ulong.Parse(parts[0]);
                    var end = ulong.Parse(parts[1]);
                    freshRanges.Add(new FreshRange(start, end));
                }
            }

            var items =
                ingredients
                .Where(ingredientId =>
                    freshRanges.Any(range => ingredientId >= range.Start && ingredientId <= range.End));

            int count = items.Count();

            var ts2 = Stopwatch.GetTimestamp();
            var elapsed = (ts2 - ts1) * 1000 / Stopwatch.Frequency;

            Console.WriteLine($"Puzzle 1: {count} ({elapsed} ms)");
        }

    }

    internal struct FreshRange
    {
        public ulong Start;
        public ulong End;

        public FreshRange(ulong start, ulong end)
        {
            Start = start;
            End = end;
        }
    }
}
