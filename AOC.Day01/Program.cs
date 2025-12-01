using AOC.Common;
using AOC.Day01.Models;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;

namespace AOC.Day01
{
    public static class Program
    {
        const int StartPosition = 50;
        static List<int> Scale = Enumerable.Range(0, 100).ToList();

        static void Main(string[] args)
        {
            Puzzle1Linq();
            // Puzzle1();
            // Puzzle2();
        }

        private static void Puzzle1()
        {
            var turnActions = LoadLists(Console.In);

            var visitedPositions = new List<Position>(turnActions.Count);

            int currentPosition = StartPosition;

            foreach (var turnAction in turnActions)
            {
                int remainingAfterFullCircles =
                    turnAction.Distance % Scale.Count;

                currentPosition = turnAction.Direction switch
                {
                    // COPILOT
                    'R' => (currentPosition + remainingAfterFullCircles) % Scale.Count,
                    'L' => (currentPosition - remainingAfterFullCircles + Scale.Count) % Scale.Count,
                    _ => currentPosition
                };

                visitedPositions.Add(new Position(Scale[currentPosition], turnAction.Distance));
            }

            Console.WriteLine($"Answer: {visitedPositions.Count(x => x.X == 0)}");
        }

        private static void Puzzle1Linq()
        {
            var turnActions = LoadLists(Console.In);

            int currentZeroCounts = 0;

            (int newPosition, int zeroCounts) results =
                turnActions
                .Aggregate(
                    (newPosition: StartPosition, zeroCounts: currentZeroCounts),
                    CalculatePosition);

            Console.WriteLine(results.zeroCounts);
        }

        private static (int newPositionResult, int zeroCounts) CalculatePosition((int currentPosition, int zeroCounts) tuple, TurnAction turnAction)
        {
            int remainingAfterFullCircles = turnAction.Distance % Scale.Count;

            var newPosition = turnAction.Direction switch
            {
                // COPILOT
                'R' => (tuple.currentPosition + remainingAfterFullCircles) % Scale.Count,
                'L' => (tuple.currentPosition - remainingAfterFullCircles + Scale.Count) % Scale.Count,
                _ => tuple.currentPosition
            };

            bool isAtZero = newPosition == 0;
            int newNumberOfZeroOccurances = isAtZero ? tuple.zeroCounts + 1 : tuple.zeroCounts;

            return (newPosition, zeroCounts: newNumberOfZeroOccurances);
        }

        private static void Puzzle2()
        {
            var turnActions = LoadLists(Console.In);

            var visitedPositions = new List<Position>(turnActions.Count);

            int currentPosition = StartPosition;

            int totalFullCirclesCount = 0;

            foreach (var turnAction in turnActions)
            {
                int fullCircles = turnAction.Distance / Scale.Count;
                totalFullCirclesCount += fullCircles;

                int remainingAfterFullCircles =
                    turnAction.Distance % Scale.Count;

                bool isCrossingZero = currentPosition != 0 && turnAction.Direction switch
                {
                    'R' => (currentPosition + remainingAfterFullCircles) > Scale.Count,
                    'L' => (currentPosition - remainingAfterFullCircles) < 0,
                    _ => false
                };

                if (isCrossingZero)
                    totalFullCirclesCount++;

                currentPosition = turnAction.Direction switch
                {
                    // COPILOT auto complete
                    'R' => (currentPosition + remainingAfterFullCircles) % Scale.Count,
                    'L' => (currentPosition - remainingAfterFullCircles + Scale.Count) % Scale.Count,
                    _ => currentPosition
                };

                visitedPositions.Add(new Position(Scale[currentPosition], turnAction.Distance));
            }

            Console.WriteLine($"Answer: {visitedPositions.Count(x => x.X == 0) + totalFullCirclesCount}");
        }


        private static List<TurnAction> LoadLists(this TextReader text) =>
            text.ReadLines().Where(x => !string.IsNullOrWhiteSpace(x)).Select(line => new TurnAction(line.Substring(0, 1)[0], int.Parse(line.Substring(1)))).ToList();
    }
}
