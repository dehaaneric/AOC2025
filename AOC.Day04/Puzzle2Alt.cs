namespace AOC.Day04
{
    internal static class Puzzle2Alt
    {
        internal static int Calc(List<string> matrix)
        {
            int totalRolls = 0;

            while (true)
            {
                var removedRolls =
                    matrix.RemoveRolls();
                if (removedRolls == 0)
                    break;

                totalRolls += removedRolls;
            }

            return totalRolls;
        }

        private static int RemoveRolls(this List<string> matrix)
        {
            int rows = matrix.Count;
            int cols = matrix[0].Length;

            // Convert rows to mutable buffers for this pass.
            var buffers = new char[rows][];
            for (int r = 0; r < rows; r++)
                buffers[r] = matrix[r].ToCharArray();

            var rowChanged = new bool[rows];
            int totalRemoved = 0;

            // Neighbor offsets in the same order as original code:
            // top1, top2, top3, center1, center3, bottom1, bottom2, bottom3
            (int dr, int dc)[] neighbors = new (int, int)[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), /* center */ (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var rowSpan = buffers[rowIndex]; // Span-friendly access
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    if (rowSpan[colIndex] != '@')
                        continue;

                    // Check the neighbor rule without allocating strings.
                    int nonDotSeen = 0;
                    int atSeen = 0;

                    for (int n = 0; n < neighbors.Length && nonDotSeen < 4; n++)
                    {
                        int nr = rowIndex + neighbors[n].dr;
                        int nc = colIndex + neighbors[n].dc;

                        if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
                            continue;

                        char c = buffers[nr][nc];
                        if (c == '.')
                            continue;

                        nonDotSeen++;
                        if (c == '@')
                            atSeen++;
                        else
                            break; // any other non-dot non-@ (rare) stops the leading sequence
                    }

                    // Equivalent to original: if the first four non-dot neighbors are '@' then skip removal.
                    bool startsWithFourAts = (nonDotSeen >= 4 && atSeen == 4);

                    if (!startsWithFourAts)
                    {
                        buffers[rowIndex][colIndex] = '.';
                        rowChanged[rowIndex] = true;
                        totalRemoved++;
                    }
                }
            }

            // Only rebuild strings for rows that changed.
            for (int r = 0; r < rows; r++)
            {
                if (rowChanged[r])
                    matrix[r] = new string(buffers[r]);
            }

            return totalRemoved;
        }
    }
}
