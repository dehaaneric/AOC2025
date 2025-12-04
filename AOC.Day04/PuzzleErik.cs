using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.Day04
{
    internal class PuzzleErik
    {
        internal static int Calc(char[][] grid)
        {
            var total = 0;
            while (true)
            {
                var changed = false;
                for (var y = 0; y < grid.Length; y++)
                {
                    for (var x = 0; x < grid.Length; x++)
                    {
                        if (grid[y][x] == '@')
                        {
                            var count = 0;
                            for (var cy = Math.Max(y - 1, 0); cy <= Math.Min(y + 1, grid.Length - 1); cy++)
                            {
                                for (var cx = Math.Max(x - 1, 0); cx <= Math.Min(x + 1, grid.Length - 1); cx++)
                                {
                                    if (grid[cy][cx] == '@')
                                    {
                                        count++;
                                    }
                                }
                            }

                            if (count <= 4)
                            {
                                grid[y][x] = 'x';
                                changed = true;
                                total++;
                            }
                        }
                    }
                }

                if (!changed)
                {
                    break;
                }
            }

            return total;
        }
    }
}
