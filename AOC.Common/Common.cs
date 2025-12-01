namespace AOC.Common
{
    public static class Common
    {
        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            while (reader.ReadLine() is string line)
            {
                yield return line;
            }
        }
    }
}
