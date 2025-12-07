using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using AOC.Day05;

namespace AOC.Benchmarks
{
    [MemoryDiagnoser]
    public class Puzzle2Benchmark
    {
        private List<string> inputs;
        [GlobalSetup]
        public void Setup()
        {
            var rng = new Random(42);
            inputs = new List<string>(100_000);
            for (int i = 0; i < 50_000; i++)
            {
                // create many overlapping and non-overlapping ranges
                ulong a = (ulong)rng.Next(0, 1_000_000);
                ulong b = a + (ulong)rng.Next(0, 1_000);
                inputs.Add($"{a}-{b}");
                // occasionally add blank lines and unrelated text to mimic real input
                if (i % 17 == 0)
                    inputs.Add(string.Empty);
                if (i % 31 == 0)
                    inputs.Add("note: ignore");
            }
        }

        [Benchmark]
        public void RunPuzzle2()
        {
            // Puzzle2.Calc mutates nothing outside inputs, pass a shallow copy to be safe
            Puzzle2.Calc(new List<string>(inputs));
        }
    }
}