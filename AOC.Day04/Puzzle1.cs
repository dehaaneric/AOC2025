
namespace AOC.Day04
{
    internal static class Puzzle1
    {
        internal static int Calc(List<string> matrix)
        {
            int rows = matrix.Count;
            int cols = matrix[0].Length;

            int totalRolls = 0;

            for (int rowIndex = 0; rowIndex < matrix.Count; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < cols; columnIndex++)
                {
                    if (matrix.IsRoll(rowIndex, columnIndex) == false)
                        continue;

                    var x = GetRollsAroundPosition(matrix, rowIndex, columnIndex);

                    if (!x.StartsWith("@@@@"))
                    {
                        // we can remove this roll
                        totalRolls++;

                        //matrix[rowIndex][columnIndex] = '.';
                        //matrix[rowIndex] = matrix[rowIndex].Substring(0, columnIndex) + '.' + matrix[rowIndex].Substring(columnIndex + 1);

                        // break;
                    }
                }
            }
            return totalRolls;
        }

        private static bool IsRoll(this List<string> matrix, int row, int col)
           => matrix[row][col] == '@';

        private static string GetRollsAroundPosition(this List<string> matrix, int row, int col)
        {
            var top1 = matrix.TryGetPosition(row - 1, col - 1);
            var top2 = matrix.TryGetPosition(row - 1, col);
            var top3 = matrix.TryGetPosition(row - 1, col + 1);

            var center1 = matrix.TryGetPosition(row, col - 1);
            //var b2 = matrix.TryGetPosition(row, col);
            var center3 = matrix.TryGetPosition(row, col + 1);

            var bottom1 = matrix.TryGetPosition(row + 1, col - 1);
            var bottom2 = matrix.TryGetPosition(row + 1, col);
            var bottom3 = matrix.TryGetPosition(row + 1, col + 1);

            return string.Join("", top1, top2, top3, center1, center3, bottom1, bottom2, bottom3)
                .Replace(".", "");
        }

        private static string TryGetPosition(this List<string> matrix, int row, int col)
        {
            if (row < 0 || row >= matrix.Count)
                return string.Empty;
            if (col < 0 || col >= matrix[0].Length)
                return string.Empty;
            return matrix[row][col].ToString();
        }
    }
}
