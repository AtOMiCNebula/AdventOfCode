// <copyright file="Day11.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day11 : DayBase
    {
        public override int Number => 11;

        public override long Part1()
        {
            return this.CalculateFinalSeatAvailability(4, (map, r, c) => CountAdjacentSeats(map, r, c, 1));
        }

        public override long Part2()
        {
            return this.CalculateFinalSeatAvailability(5, (map, r, c) => CountAdjacentSeats(map, r, c, int.MaxValue));
        }

        public static int CountAdjacentSeatsSearcher(IList<string> map, int r, int c, int rDelta, int cDelta, int viewDistance)
        {
            for (int i = 0; i < viewDistance; i++)
            {
                r += rDelta;
                c += cDelta;

                if (0 <= r && r < map.Count && 0 <= c && c < map[r].Length)
                {
                    if (map[r][c] == '#')
                    {
                        return 1;
                    }
                    else if (map[r][c] == 'L')
                    {
                        return 0;
                    }
                }
                else
                {
                    break;
                }
            }

            return 0;
        }

        public static int CountAdjacentSeats(IList<string> map, int r, int c, int viewDistance)
        {
            return
                CountAdjacentSeatsSearcher(map, r, c, -1, -1, viewDistance) + // Northwest
                CountAdjacentSeatsSearcher(map, r, c, -1, 0, viewDistance) + // North
                CountAdjacentSeatsSearcher(map, r, c, -1, 1, viewDistance) + // Northeast
                CountAdjacentSeatsSearcher(map, r, c, 0, -1, viewDistance) + // West
                CountAdjacentSeatsSearcher(map, r, c, 0, 1, viewDistance) + // East
                CountAdjacentSeatsSearcher(map, r, c, 1, -1, viewDistance) + // Southwest
                CountAdjacentSeatsSearcher(map, r, c, 1, 0, viewDistance) + // South
                CountAdjacentSeatsSearcher(map, r, c, 1, 1, viewDistance); // Southeast
        }

        public int CalculateFinalSeatAvailability(int adjacentLimit, Func<IList<string>, int, int, int> adjacencyCalculator)
        {
            bool stable;
            IList<string> map = this.GetInputRaw().ToList();

            do
            {
                stable = true;

                List<string> newMap = new();
                for (int r = 0; r < map.Count; r++)
                {
                    StringBuilder newRow = new();

                    for (int c = 0; c < map[r].Length; c++)
                    {
                        int adjacent = adjacencyCalculator(map, r, c);
                        if (map[r][c] == 'L' && adjacent == 0)
                        {
                            newRow.Append('#');
                            stable = false;
                        }
                        else if (map[r][c] == '#' && adjacent >= adjacentLimit)
                        {
                            newRow.Append('L');
                            stable = false;
                        }
                        else
                        {
                            newRow.Append(map[r][c]);
                        }
                    }

                    newMap.Add(newRow.ToString());
                }

                map = newMap;
            }
            while (!stable);

            // Count occupied seats
            return map.Sum(r => r.Sum(s => s == '#' ? 1 : 0));
        }
    }
}
