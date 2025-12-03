using AOC.Common;

namespace AOC.Day03
{
    internal static class Puzzle2
    {
        const int BatterBankLength = 12;
        public static ulong Calc()
        {
            List<string> batteryBanks = LoadInput(Console.In);

            var value = PuzzleRefactor.Calc(batteryBanks, 12);

            ulong totalValue = 0;

            foreach (var bank in batteryBanks)
            {
                var finalNumbers = new List<ulong>();

                var length = bank.Length;

                ulong[] numbers =
                    bank.ToCharArray().Select(x => ulong.Parse(x.ToString())).ToArray();

                for (int index = 0; index < length; index++)
                {
                    var neededAtTheEnd = BatterBankLength - (index + 1);
                    if (neededAtTheEnd < 0)
                        break;

                    var indexesToTurn = numbers[..^neededAtTheEnd];

                    var highestNumberToFind = indexesToTurn.Max();
                    var indexOfHighestNumber = indexesToTurn.IndexOf(highestNumberToFind);

                    finalNumbers.Add(indexesToTurn[indexOfHighestNumber]);

                    numbers = numbers[(indexOfHighestNumber + 1)..];
                }

                ulong parsedNumber
                    = ulong.Parse(string.Join("", finalNumbers));
                totalValue += parsedNumber;
            }

            return totalValue;
        }
        private static List<string> LoadInput(this TextReader text) =>
            text.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
