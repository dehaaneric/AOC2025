using NUnit.Framework;
using System.IO;

namespace AOC.Day06
{
    [TestFixture]
    public class Day06Tests
    {
        [Test]
        [TestCase("Puzzle1Test.txt", 4277556UL)]
        [TestCase("Puzzle1Prod.txt", 5316572080628UL)]
        public void Puzzle1_FileAsInput(string fileName, ulong expectedValue)
        {
            // Resolve file in the test output directory
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            string[] inputlines =
                File.ReadAllLines(filePath);

            ulong[][] numbers = new ulong[inputlines.Length - 1][];

            for (int lineIndex = 0; lineIndex < inputlines.Length - 1; lineIndex++)
            {
                var line = inputlines[lineIndex];

                // Process each line as needed
                // split the numbers in Line by one or more spaces efficiently
                ulong[] parts =
                    line
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ulong.Parse)
                        .ToArray();

                numbers[lineIndex] = new ulong[parts.Length];

                var partsCount = parts.Count();
                for (int partsIndex = 0; partsIndex < partsCount; partsIndex++)
                {
                    numbers[lineIndex][partsIndex] = parts[partsIndex];
                }
            }

            char[] mathOperations =
            inputlines[^1]
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(char.Parse)
                .ToArray();

            var mathProblems = new List<MathProblem>();

            int numberOfColumns = numbers[0].Length;
            int numberOfRows = numbers.Length;
            for (int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
            {
                var mathProblem = new MathProblem(
                    new List<ulong>(),
                    mathOperations[columnIndex].ToString());
                for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
                {
                    // Process each number as needed
                    ulong number = numbers[rowIndex][columnIndex];
                    mathProblem.Numbers.Add(number);
                }

                mathProblem.MathOperator = mathOperations[columnIndex].ToString();
                mathProblems.Add(mathProblem);
            }

            ulong finalResult = mathProblems.Select(mp => mp.Solve())
                .Aggregate((acc, val) => acc + val);

            Assert.That(finalResult, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase("Puzzle2Test.txt", 3263827UL)]
        [TestCase("Puzzle2Prod.txt", 11299263623062UL)]
        public void Puzzle2_FileAsInput(string fileName, ulong expectedValue)
        {
            // Resolve file in the test output directory
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            string[] inputlines =
                File.ReadAllLines(filePath);

            int maxLength = inputlines.Select(line => line.Length).Max();

            var allValues = new List<List<string>>();
            for (int colIndex = 0; colIndex < maxLength; colIndex++)
            {
                List<string> values = new List<string>();

                // Process each column as needed
                foreach (var line in inputlines)
                {
                    //var ch = line.Length > colIndex ? line[colIndex] : ' ';
                    values.Add(line[colIndex].ToString());
                }

                allValues.Add(values);
            }

            NumberGroup? numberGroup = null;
            List<NumberGroup> numberGroups = new List<NumberGroup>();
            for (int i = 0; i < allValues.Count; i++)
            {
                List<string>? value = allValues[i];
                if (value.All(v => string.IsNullOrWhiteSpace(v)))
                {
                    numberGroups.Add(numberGroup!.Value);
                    numberGroup = null;
                    // new block
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(value.Last()))
                {
                    numberGroup = new NumberGroup(
                        new List<ulong>(),
                        value.Last().Trim());
                }

                // We have a math operator
                ulong number
                    = string.Join(string.Empty, value.Take(value.Count - 1))
                        .Trim() == string.Empty
                        ? 0
                        : ulong.Parse(string.Join(string.Empty, value.Take(value.Count - 1)).Trim());
                numberGroup!.Value.Numbers.Add(number);

                if (i == allValues.Count - 1 && numberGroup.HasValue)
                {
                    numberGroups.Add(numberGroup!.Value);
                }
            }

            ulong totals = 0;
            foreach (var numberedGroup in numberGroups)
            {
                ulong v = numberedGroup.Solve();
                totals += v;
            }

            Assert.That(totals, Is.EqualTo(expectedValue));
        }
    }
    public struct MathProblem
    {
        public List<ulong> Numbers = new List<ulong>();
        public string MathOperator = string.Empty;

        public MathProblem(List<ulong> numbers, string mathOperator)
        {
            Numbers = numbers;
            MathOperator = mathOperator;
        }

        public ulong Solve()
        {
            if (Numbers.Count == 0)
            {
                return 0;
            }

            ulong result = Numbers[0];
            for (int i = 1; i < Numbers.Count; i++)
            {
                result = Calculate(MathOperator, result, Numbers[i]);
            }

            return result;
        }

        public ulong Calculate(string operatorType, ulong v1, ulong v2)
        {
            return operatorType switch
            {
                "+" => v1 + v2,
                "-" => v1 - v2,
                "*" => v1 * v2,
                "/" => v1 / v2,
                _ => 0,
            };
        }
    }

    public struct NumberGroup
    {
        public List<ulong> Numbers;
        public string MathOperator;

        public NumberGroup(
            List<ulong> numbers,
            string mathOperator)
        {
            Numbers = numbers;
            MathOperator = mathOperator;
        }

        public ulong Solve()
        {
            if (Numbers.Count == 0)
            {
                return 0;
            }

            ulong result = Numbers[0];
            for (int i = 1; i < Numbers.Count; i++)
            {
                result = Calculate(MathOperator, result, Numbers[i]);
            }

            return result;
        }

        private ulong Calculate(string operatorType, ulong v1, ulong v2)
        {
            return operatorType switch
            {
                "+" => v1 + v2,
                "-" => v1 - v2,
                "*" => v1 * v2,
                "/" => v1 / v2,
                _ => 0,
            };
        }
    }

}
