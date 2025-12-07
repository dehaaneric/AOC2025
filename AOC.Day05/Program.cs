using AOC.Common;

namespace AOC.Day05
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var inputs = LoadInput(Console.In);

            //Puzzle1.Calc(inputs);
            Puzzle2.Calc(inputs);
        }
        private static List<string> LoadInput(this TextReader text) =>
            text.ReadLines().ToList();
    }
}
