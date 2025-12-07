using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Numerics;

namespace AOC.Day07
{
    public class Day7PuzzleTests
    {
        [Test]
        [TestCase("Puzzle1Test.txt", 21UL)]
        [TestCase("Puzzle1Prod.txt", 1570UL)]
        public void Puzzle1Test(string fileName, ulong expectedValue)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            string[] inputlines =
                File.ReadAllLines(filePath);

            var rowCount = inputlines.Length;
            char[][] grid = new char[rowCount][];
            for (int lineIndex = 0; lineIndex < inputlines.Length; lineIndex++)
            {
                var line = inputlines[lineIndex];
                grid[lineIndex] = line.ToCharArray();
            }

            int columnCount = grid[0].Length;
            int splitCounter = 0;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var row = grid[rowIndex];
                for (int colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    var cell = row[colIndex];
                    if (cell == 'S')
                    {
                        // Lower the beam
                        grid[rowIndex + 1][colIndex] = '|';
                    }
                    if (ShouldSplit(grid, rowIndex, colIndex))
                    {
                        splitCounter++;

                        var leftPosition = Math.Max(0, colIndex - 1);
                        var rightPosition = Math.Min((columnCount - 1), colIndex + 1);

                        grid[rowIndex][leftPosition] = '|';
                        grid[rowIndex][rightPosition] = '|';

                        AddBeamTillEndOrSplitter(grid, rowIndex + 1, leftPosition, rightPosition);
                    }

                }

                DebugPrintLines(rowIndex, grid);
            }

            // Print the grid for visualization
            Assert.That(splitCounter, Is.EqualTo(expectedValue));
        }

        private void DebugPrintLines(int untilRow, char[][] grid)
        {
            Debug.WriteLine(string.Concat(untilRow.ToString("00"), new string(grid[untilRow])));
            Console.WriteLine(string.Concat(untilRow.ToString("00"), new string(grid[untilRow])));
        }
        private void AddBeamTillEndOrSplitter(char[][] grid, int startRowIndex, int leftPosition, int rightPosition)
        {
            bool continueLeft = true;
            bool continueRight = true;

            for (int rowIndex = startRowIndex; rowIndex < grid.Length; rowIndex++)
            {
                if (!continueLeft && !continueRight)
                {
                    break;
                }

                if (continueLeft && grid[rowIndex][leftPosition] == '.')
                {
                    grid[rowIndex][leftPosition] = '|';
                }
                else
                {
                    continueLeft = false;
                }

                if (continueRight && grid[rowIndex][rightPosition] == '.')
                {
                    grid[rowIndex][rightPosition] = '|';
                }
                else
                {
                    continueRight = false;
                }
            }
        }
        private bool ShouldSplit(char[][] grid, int rowIndex, int columnIndex)
        {
            var cellValue = grid[rowIndex][columnIndex];
            bool isSplitter = cellValue == '^';

            bool hasBeamAbove = grid[Math.Max(0, rowIndex - 1)][columnIndex] == '|';

            return isSplitter && hasBeamAbove;
        }
    }
}
