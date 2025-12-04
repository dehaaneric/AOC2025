//using AOC.Common;

//namespace AOC.Day04
//{
//    internal static class Program
//    {
//        static void Main(string[] args)
//        {
//            //List<string> matrix = Console.In.ReadLines().ToList();
//            List<string> matrix =
//            [
//                "..@@.@@@@.",
//                "@@@.@.@.@@",
//                "@@@@@.@.@@",
//                "@.@@@@..@.",
//                "@@.@@@@.@@",
//                ".@@@@@@@.@",
//                ".@.@.@.@@@",
//                "@.@@@.@@@@",
//                ".@@@@@@@@.",
//                "@.@.@@@.@.",
//            ];

//            int rows = matrix.Count;
//            int cols = matrix[0].Length;
//            var allStrings = GetAllStrings(matrix, rows, cols);

//            for (int rowIndex = 0; rowIndex < matrix.Count; rowIndex++)
//            {
//                string row = matrix[rowIndex];
//                for (int columnIndex = 0; columnIndex < cols; columnIndex++)
//                {
//                    var diagonals =
//                        GetDiagnoalCharactersFromMatrixFromStartingPointAll(
//                            matrix,
//                            rowIndex,
//                            columnIndex);

//                    var axisStrings = GetTopBottomLeftRightCharactersFromStartingPoint(
//                        matrix,
//                        rowIndex,
//                        columnIndex,
//                        rows, cols);

//                    axisStrings.AddRange(diagonals);

//                    if (AreAllValid(axisStrings))
//                    {
//                        // Register posistion
//                    }

//                }
//            }


//            Console.WriteLine("Hello, World!");
//        }

//        private static bool AreAllValid(List<string> axisStrings)
//        {
//            return
//                axisStrings
//                .All(s =>
//                    !string.Equals(
//                        s.Substring(0, int.Min(4, s.Length)), "@@@@")
//                );
//        }

//        private static List<string> GetTopBottomLeftRightCharactersFromStartingPoint(
//            List<string> matrix,
//            int rowIndex,
//            int columnIndex,
//            int rowCount,
//            int columnCount)
//        {
//            IEnumerable<char> columnValues =
//                Enumerable.Range(0, rowCount)
//                    .Select((rowIndex) => matrix[rowIndex][columnIndex])
//                    .ToList(); // All vertical values in the column

//            string topPart =
//                new string(
//                    columnValues
//                        .Reverse()
//                        .Skip(rowCount - rowIndex)
//                        .ToArray());

//            string bottomPart =
//                new string(
//                    columnValues
//                        .Skip(rowIndex+1)
//                        .ToArray());

//            var leftPart =
//                new string(
//                    matrix[rowIndex]
//                        .Substring(0, columnIndex)
//                        .Reverse()
//                        .ToArray()); // All horizontal values to the left
//            var rightPart =
//                new string(
//                    matrix[rowIndex]
//                        .Substring(columnIndex + 1, columnCount - columnIndex - 1)
//                        .ToArray()); // All horizontal values to the right

//            return [
//                topPart,
//                bottomPart,
//                leftPart,
//                rightPart
//                ];
//        }

//        public static List<string> GetDiagnoalCharactersFromMatrixFromStartingPointAll(
//            IEnumerable<string> matrix,
//            int startRow,
//            int startColumn)
//        {
//            return [
//                GetDiagnoalCharactersFromMatrixFromStartingPoint(
//                    matrix,
//                    startRow,
//                    startColumn,
//                    down: true,
//                    right: true).Substring(1),
//                GetDiagnoalCharactersFromMatrixFromStartingPoint(
//                    matrix,
//                    startRow,
//                    startColumn,
//                    down: true,
//                    right: false).Substring(1),
//                GetDiagnoalCharactersFromMatrixFromStartingPoint(
//                    matrix,
//                    startRow,
//                    startColumn,
//                    down: false,
//                    right: true).Substring(1),
//                GetDiagnoalCharactersFromMatrixFromStartingPoint(
//                    matrix,
//                    startRow,
//                    startColumn,
//                    down: false,
//                    right: false).Substring(1)
//                ];
//        }
//        public static string GetDiagnoalCharactersFromMatrixFromStartingPoint(
//            IEnumerable<string> matrix,
//            int startRow,
//            int startColumn,
//            bool down,
//            bool right)
//        {
//            int rows = matrix.Count();
//            int cols = matrix.First().Length;
//            List<char> diagonalChars = new();
//            int currentRow = startRow;
//            int currentCol = startColumn;
//            while (currentRow >= 0 && currentRow < rows && currentCol >= 0 && currentCol < cols)
//            {
//                diagonalChars.Add(matrix.ElementAt(currentRow)[currentCol]);
//                currentRow += down ? 1 : -1;
//                currentCol += right ? 1 : -1;
//            }
//            return new string(diagonalChars.ToArray());
//        }

//        private static bool IsRoll(char cell)
//            => cell == '@';

//        private static IEnumerable<string> GetAllStrings(this IEnumerable<string> matrix, int rows, int cols) =>
//            matrix.Rows().Concat(matrix.Columns(cols)).Concat(matrix.Diagonals(rows, cols)).Concat(matrix.Antidiagonals(rows, cols));// .TwoWay();

//        private static IEnumerable<string> Diagonals(this IEnumerable<string> matrix, int rows, int cols) =>
//            Enumerable.Range(0, cols).Select(col => matrix.Diagonal(0, col, cols))
//                .Concat(Enumerable.Range(1, rows - 1).Select(row => matrix.Diagonal(row, 0, cols)));

//        private static IEnumerable<string> Antidiagonals(this IEnumerable<string> matrix, int rows, int cols) =>
//            matrix.Reverse().Diagonals(rows, cols);

//        private static string Diagonal(this IEnumerable<string> matrix, int startRow, int startCol, int cols) =>
//            new string(matrix.Skip(startRow).Take(cols - startCol).Select((row, i) => row[startCol + i]).ToArray());

//        private static IEnumerable<string> Rows(this IEnumerable<string> matrix) => matrix;
//        private static IEnumerable<string> Columns(this IEnumerable<string> matrix, int cols) =>
//            Enumerable.Range(0, cols).Select(i => new string(matrix.Select(row => row[i]).ToArray()));
//    }
//}
