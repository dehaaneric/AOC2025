using AOC.Common;

namespace AOC.Day03
{
    internal static class Puzzle1
    {
        public static ulong Calc()
        {
            var inputs = LoadInput(Console.In);

            ulong totalCount = 0;

            foreach (var line in inputs)
            {
                // Process each line
                var hashset = new HashSet<ulong>();
                GenerateCombinations(line.ToCharArray(), hashset);

                totalCount += hashset.Max();
            }

            return totalCount;
        }

        private static void GenerateCombinations(
            ReadOnlySpan<char> values,
            HashSet<ulong> results)
        {
            if (values.Length < 1)
                return;

            // Continue adding from next items
            for (int i = 1; i < values.Length; i++)
            {
                char value = values[i];
                results.Add(ulong.Parse(values[0] + value.ToString()));
            }

            GenerateCombinations(values.Slice(1), results);
        }

        private static List<string> LoadInput(this TextReader text) =>
            text.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
