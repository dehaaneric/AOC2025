using AOC.Common;

namespace AOC.Day02
{
    internal static class Puzzle1
    {
        internal static long Calc()
        {
            var inputs = LoadInput(Console.In);
            var input = inputs[0];

            var resultPuzzle1 =
                input
                    .Split(',')
                    .Select(s => s.Split('-') switch { var a => (long.Parse(a[0]), long.Parse(a[1])) })
                    .Select(GetIntRange)
                    .Select(GetDoubleCharacters)
                    .SelectMany(x => x)
                    .Sum();

            return resultPuzzle1;
        }

        private static IEnumerable<long> GetDoubleCharacters(this IEnumerable<long> source)
        {
            foreach (var item in source.Select(x => x.ToString()))
            {
                var length = item.Length;
                if (length % 2 == 0)
                {
                    // It's even
                    var leftPart = item.Substring(0, length / 2);
                    var rightPart = item.Substring(length / 2);
                    bool areEqual = string.Equals(leftPart, rightPart);
                    if (areEqual)
                    {
                        yield return long.Parse(item);
                    }
                }
            }
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
