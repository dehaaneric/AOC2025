using AOC.Common;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace AOC.Day07
{
    public class Day7Puzzle2Tests
    {
        [Test]
        [TestCase("Puzzle2Test.txt", 40UL)]
        //[TestCase("Puzzle2Prod.txt", 15118009521693UL)]
        public static void PuzzleTest(string fileName, ulong expectedValue)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            string[] inputlines =
                File.ReadAllLines(filePath);

            var manifold = ReadManifold(inputlines);

            var (totalSplits, totalBeams) = manifold.Simulate();

            Console.WriteLine($"Total splits encountered: {totalSplits}");
            Console.WriteLine($"Total beams at the end:   {totalBeams}");

            // Print the grid for visualization
            Assert.That(totalBeams, Is.EqualTo(expectedValue));
        }

        private static Manifold ReadManifold(string[] input)
        {
            int rowsCount = 0;
            Position? start = null;
            HashSet<Position> splitters = new();

            foreach (var line in input)
            {
                foreach (var pos in line.Extract(rowsCount, 'S')) start = pos;
                foreach (var pos in line.Extract(rowsCount, '^')) splitters.Add(pos);
                rowsCount++;
            }

            if (!start.HasValue) throw new InvalidDataException("Start position not found.");

            return new Manifold(rowsCount, start.Value, splitters);
        }
    }

    public record struct Beam(Position Position, ulong Count);
    public record struct Position(int Row, int Column);
    public record struct Manifold(int RowsCount, Position Start, HashSet<Position> Splitters);
    public static class Extensions
    {
        public static (int totalSplits, ulong totalBeams) Simulate(this Manifold manifold)
        {
            var beams = manifold.Start().ToList();
            var totalSplits = 0;
            var totalBeams = beams.SumCounts();

            while (true)
            {
                totalSplits += manifold.CountSplits(beams);
                var nextBeams = manifold.Move(beams).ToList();
                if (nextBeams.Count == 0) break;

                beams = nextBeams;
                totalBeams = beams.SumCounts();
            }

            return (totalSplits, totalBeams);
        }

        public static string Format(this IEnumerable<Beam> beams) =>
            string.Join(" ", beams.Select(beam => $"({beam.Position.Column}/{beam.Count})"));

        public static IEnumerable<Beam> Move(this Manifold manifold, IEnumerable<Beam> beams) =>
            beams.Select(Move)
                .Where(beam => beam.Position.Row < manifold.RowsCount)
                .SelectMany(beam => manifold.Splitters.Contains(beam.Position) ? beam.Split() : [beam])
                .GroupBy(beam => beam.Position, (pos, beams) => new Beam(pos, beams.SumCounts()));

        public static ulong SumCounts(this IEnumerable<Beam> beams) =>
            beams.Aggregate(0UL, (acc, beam) => acc + beam.Count);

        public static int CountSplits(this Manifold manifold, IEnumerable<Beam> beams) =>
            beams.Select(Move).Count(beam => manifold.Splitters.Contains(beam.Position));

        public static IEnumerable<Beam> Start(this Manifold manifold) =>
            [new Beam(manifold.Start, 1)];

        public static IEnumerable<Beam> Split(this Beam beam) =>
            beam.Position.Split().Select(pos => beam with { Position = pos });

        public static IEnumerable<Position> Split(this Position beam) =>
            [beam with { Column = beam.Column - 1 }, beam with { Column = beam.Column + 1 }];

        public static Beam Move(this Beam beam) =>
            beam with { Position = beam.Position.Move() };

        public static Position Move(this Position beam) =>
            beam with { Row = beam.Row + 1 };
        public static IEnumerable<Position> Extract(this string line, int row, char target) =>
            line.Select((ch, col) => (ch, col))
                .Where(t => t.ch == target)
                .Select(t => new Position(row, t.col));
    }
}
