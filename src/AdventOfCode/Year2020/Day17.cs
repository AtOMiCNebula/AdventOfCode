// <copyright file="Day17.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Day17 : DayBase
    {
        public override int Number => 17;

        public override long Part1()
        {
            return Day17.EvaluateCycles(6, this.GetInputRaw());
        }

        public override long Part2()
        {
            return -1;
        }

        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "https://github.com/dotnet/roslyn-analyzers/issues/4637")]
        public static long EvaluateCycles(int cycles, IEnumerable<string> initialSlices)
        {
            HashSet<(int X, int Y, int Z)> state = new HashSet<(int X, int Y, int Z)>();
            IList<string> initialSlicesList = initialSlices.ToList();
            for (int y = 0; y < initialSlicesList.Count; y++)
            {
                for (int x = 0; x < initialSlicesList[y].Length; x++)
                {
                    if (initialSlicesList[y][x] == '#')
                    {
                        state.Add((x, y, 0));
                    }
                }
            }

            for (int i = 0; i < cycles; i++)
            {
                // Determine coordinate bounds
                int xMin = int.MaxValue;
                int xMax = int.MinValue;
                int yMin = int.MaxValue;
                int yMax = int.MinValue;
                int zMin = int.MaxValue;
                int zMax = int.MinValue;
                foreach ((int x, int y, int z) in state)
                {
                    xMin = Math.Min(xMin, x);
                    xMax = Math.Max(xMax, x);
                    yMin = Math.Min(yMin, y);
                    yMax = Math.Max(yMax, y);
                    zMin = Math.Min(zMin, z);
                    zMax = Math.Max(zMax, z);
                }

                // Print out state
                /*Console.WriteLine($"Cycle {i}");
                for (int z = zMin; z <= zMax; z++)
                {
                    Console.WriteLine($"z={z}");
                    for (int y = yMin; y <= yMax; y++)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            Console.Write(state.Contains((x, y, z)) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }*/

                // Increase testing bounds
                xMin -= 1;
                xMax += 1;
                yMin -= 1;
                yMax += 1;
                zMin -= 1;
                zMax += 1;

                // Evaluate new state
                HashSet<(int X, int Y, int Z)> newState = new HashSet<(int X, int Y, int Z)>();
                for (int z = zMin; z <= zMax; z++)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        for (int x = xMin; x <= xMax; x++)
                        {
                            int neighbors = 0;
                            for (int zDelta = -1; zDelta <= 1; zDelta++)
                            {
                                for (int yDelta = -1; yDelta <= 1; yDelta++)
                                {
                                    for (int xDelta = -1; xDelta <= 1; xDelta++)
                                    {
                                        if (zDelta == 0 && yDelta == 0 && xDelta == 0)
                                        {
                                            continue;
                                        }

                                        neighbors += state.Contains((x + xDelta, y + yDelta, z + zDelta)) ? 1 : 0;
                                    }
                                }
                            }

                            if (state.Contains((x, y, z)) && (neighbors == 2 || neighbors == 3))
                            {
                                newState.Add((x, y, z));
                            }
                            else if (!state.Contains((x, y, z)) && neighbors == 3)
                            {
                                newState.Add((x, y, z));
                            }
                        }
                    }
                }
                state = newState;
            }

            return state.Count;
        }
    }
}
