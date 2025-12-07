using AOC.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AOC.Day05
{
    internal static class Puzzle2
    {
        internal static void Calc(List<string> inputs)
        {
            var ts1 = Stopwatch.GetTimestamp();

            var length = inputs.Count;

            var freshRanges = new List<FreshRange>(length);

            foreach (var line in inputs)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var dashIdx = line.IndexOf('-');
                if (dashIdx > 0)
                {
                    var span = line.AsSpan();
                    var left = span.Slice(0, dashIdx);
                    var right = span.Slice(dashIdx + 1);

                    if (TryParseUlong(left, out var start) && TryParseUlong(right, out var end))
                    {
                        freshRanges.Add(new FreshRange(start, end));
                    }
                }
            }

            // Sort by start and merge in a single pass to avoid repeated scans and LINQ allocations.
            freshRanges.Sort((a, b) => a.Start.CompareTo(b.Start));

            var aggregatedRanges = new List<FreshRange>(freshRanges.Count);

            if (freshRanges.Count > 0)
            {
                var current = freshRanges[0];

                for (int i = 1; i < freshRanges.Count; i++)
                {
                    var r = freshRanges[i];

                    // If overlapping or contiguous, merge
                    if (r.Start <= current.End + 1)
                    {
                        var maxEnd = Math.Max(current.End, r.End);
                        current.End = maxEnd;
                    }
                    else
                    {
                        aggregatedRanges.Add(current);
                        current = r;
                    }
                }

                aggregatedRanges.Add(current);
            }

            ulong total = 0;
            foreach (var newrange in aggregatedRanges)
            {
                total += (newrange.End - newrange.Start + 1);
            }

            var ts2 = Stopwatch.GetTimestamp();
            var elapsed = (ts2 - ts1) * 1000 / Stopwatch.Frequency;

            Console.WriteLine($"Puzzle 2: {total} ({elapsed} ms)");
        }

        private static bool TryParseUlong(ReadOnlySpan<char> span, out ulong value)
        {
            value = 0;
            if (span.Length == 0)
                return false;

            int i = 0;
            int j = span.Length - 1;

            // trim spaces
            while (i <= j && span[i] == ' ') i++;
            while (j >= i && span[j] == ' ') j--;
            if (i > j) return false;

            for (int k = i; k <= j; k++)
            {
                char c = span[k];
                if (c < '0' || c > '9')
                    return false;

                int digit = c - '0';
                // check overflow: value * 10 + digit > ulong.MaxValue
                if (value > (ulong.MaxValue - (ulong)digit) / 10)
                    return false;

                value = value * 10 + (ulong)digit;
            }

            return true;
        }
    }
}
