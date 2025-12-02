using AOC.Common;

namespace AOC.Day02
{
    public static class Puzzle2
    {
        public static long Calc()
        {
            var inputs = LoadInput(Console.In);
            var input = inputs[0];

            var resultPuzzle2 =
                input
                    .Split(',')
                    .Select(s => s.Split('-') switch { var a => (long.Parse(a[0]), long.Parse(a[1])) })
                    .Select(GetIntRange)
                    .Select(GetDoubleCharacters)
                    .SelectMany(x => x)
                    .Sum();

            return resultPuzzle2;
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        private static IEnumerable<long> GetDoubleCharacters(this IEnumerable<long> source)
        {
            List<long> sum = new List<long>();

            foreach(var item in source.Select(x => x.ToString()))
            {
                int maxChunkSize = item.Length / 2;

                for (int chunkSize = 1; chunkSize <= maxChunkSize; chunkSize++)
                {
                    if (item.Length % chunkSize != 0)
                        continue;

                    var chunks = Split(item, chunkSize).ToList();
                    bool allEqual = chunks.All(c => c == chunks[0]);
                    if (allEqual)
                    {
                        sum.Add(long.Parse(item));
                        break;
                    }
                }
            }

            return sum;
        }

        private static IEnumerable<long> GetIntRange(this (long, long) source)
        {
            var collectionSize = (int)(source.Item2 - source.Item1 + 1);
            var items = new List<long>();

            for (long i = source.Item1; i <= source.Item2; i++)
            {
                items.Add(i);
            }

            return items;
        }

        private static List<string> LoadInput(this TextReader text) =>
            text.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
