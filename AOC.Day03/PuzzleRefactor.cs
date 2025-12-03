namespace AOC.Day03
{
    internal static class PuzzleRefactor
    {
        public static ulong Calc(List<string> batteryBanks, int remainingslots)
        {
            ulong totalValue = 0;

            foreach (var bank in batteryBanks)
            {
                var finalNumbers = new List<ulong>();

                var bankLength = bank.Length;
                ulong[] numbers =
                    bank.ToCharArray().Select(x => ulong.Parse(x.ToString())).ToArray();

                for (int index = 0; index < bankLength; index++)
                {
                    var neededSlotsLeftOver = remainingslots - (index + 1);
                    if (neededSlotsLeftOver < 0)
                        break;

                    var indexesToTurn = numbers[..^neededSlotsLeftOver];

                    var highestNumberToFind = indexesToTurn.Max();
                    var indexOfHighestNumber = indexesToTurn.IndexOf(highestNumberToFind);

                    finalNumbers.Add(indexesToTurn[indexOfHighestNumber]);

                    numbers = numbers.Skip(indexOfHighestNumber + 1).ToArray();
                }

                ulong parsedNumber
                    = ulong.Parse(string.Join("", finalNumbers));
                totalValue += parsedNumber;
            }

            return totalValue;
        }
    }
}
