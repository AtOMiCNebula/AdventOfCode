// <copyright file="Day17.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day17 : DayBase
    {
        public override int Number => 17;

        public override long Part1()
        {
            return Day17.EvaluateCycles<Hypercube3D>(6, this.GetInputRaw());
        }

        public override long Part2()
        {
            return Day17.EvaluateCycles<Hypercube4D>(6, this.GetInputRaw());
        }

        public static long EvaluateCycles<THypercube>(int cycles, IEnumerable<string> initialSlices)
            where THypercube : HypercubeBase, new()
        {
            ISet<THypercube> state = new SortedSet<THypercube>(HypercubeSorter.Default);
            IList<string> initialSlicesList = initialSlices.ToList();
            for (int y = 0; y < initialSlicesList.Count; y++)
            {
                for (int x = 0; x < initialSlicesList[y].Length; x++)
                {
                    if (initialSlicesList[y][x] == '#')
                    {
                        state.Add(new THypercube
                        {
                            X = x,
                            Y = y,
                        });
                    }
                }
            }

            for (int i = 0; i < cycles; i++)
            {
                // Determine coordinate bounds
                THypercube minimums = new();
                THypercube maximums = new();
                foreach (THypercube hypercube in state)
                {
                    minimums.Aggregate(hypercube, Math.Min);
                    maximums.Aggregate(hypercube, Math.Max);
                }

                // Print out state
                /*Console.WriteLine($"Cycle {i}");
                DimensionMultiply(minimums, maximums, hypercube =>
                {
                    if (hypercube.X == minimums.X && hypercube.Y == minimums.Y)
                    {
                        Console.WriteLine($"z={(hypercube as Hypercube3D).Z}");
                    }

                    Console.Write(state.Contains(hypercube) ? '#' : '.');

                    if (hypercube.X == maximums.X)
                    {
                        Console.WriteLine();
                        if (hypercube.Y == maximums.Y)
                        {
                            Console.WriteLine();
                        }
                    }
                });*/

                // Increase testing bounds
                for (int d = 0; d < minimums.Dimensions; d++)
                {
                    minimums[d] -= 1;
                    maximums[d] += 1;
                }

                THypercube deltaLower = new();
                THypercube deltaUpper = new();
                for (int d = 0; d < deltaLower.Dimensions; d++)
                {
                    deltaLower[d] = -1;
                    deltaUpper[d] = 1;
                }

                // Evaluate new state
                ISet<THypercube> newState = new SortedSet<THypercube>(HypercubeSorter.Default);
                DimensionMultiply(minimums, maximums, hypercube =>
                {
                    int neighbors = 0;
                    DimensionMultiply(deltaLower, deltaUpper, hypercubeDelta =>
                    {
                        if (hypercubeDelta.IsZero())
                        {
                            return;
                        }

                        THypercube combined = HypercubeBase.Combine(hypercube, hypercubeDelta);
                        neighbors += state.Contains(combined) ? 1 : 0;
                    });

                    if (state.Contains(hypercube) && (neighbors == 2 || neighbors == 3))
                    {
                        newState.Add(hypercube.Clone<THypercube>());
                    }
                    else if (!state.Contains(hypercube) && neighbors == 3)
                    {
                        newState.Add(hypercube.Clone<THypercube>());
                    }
                });
                state = newState;
            }

            return state.Count;
        }

        public static void DimensionMultiply<THypercube>(THypercube rangeLower, THypercube rangeUpper, Action<THypercube> action)
            where THypercube : HypercubeBase, new()
        {
            Day17.DimensionMultiply(rangeLower, rangeUpper, action, new THypercube(), rangeLower.Dimensions - 1);
        }

        public static void DimensionMultiply<THypercube>(THypercube rangeLower, THypercube rangeUpper, Action<THypercube> action, THypercube current, int currentDimension)
            where THypercube : HypercubeBase
        {
            for (int i = rangeLower[currentDimension]; i <= rangeUpper[currentDimension]; i++)
            {
                current[currentDimension] = i;

                if (currentDimension == 0)
                {
                    action(current);
                }
                else
                {
                    Day17.DimensionMultiply(rangeLower, rangeUpper, action, current, currentDimension - 1);
                }
            }
        }
    }

    public abstract class HypercubeBase
    {
        public abstract int Dimensions { get; }

        public int X { get; set; }

        public int Y { get; set; }

        public virtual int this[int dimension]
        {
            get
            {
                return dimension switch
                {
                    0 => this.X,
                    1 => this.Y,
                    _ => throw new NotSupportedException(),
                };
            }

            set
            {
                if (dimension == 0)
                {
                    this.X = value;
                }
                else if (dimension == 1)
                {
                    this.Y = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public bool IsZero()
        {
            for (int d = 0; d < this.Dimensions; d++)
            {
                if (this[d] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Aggregate(HypercubeBase other, Func<int, int, int> aggregator)
        {
            for (int d = 0; d < this.Dimensions; d++)
            {
                this[d] = aggregator(this[d], other[d]);
            }
        }

        public static THypercube Combine<THypercube>(THypercube left, THypercube right)
            where THypercube : HypercubeBase, new()
        {
            THypercube result = new();
            for (int d = 0; d < result.Dimensions; d++)
            {
                result[d] = left[d] + right[d];
            }
            return result;
        }

        public THypercube Clone<THypercube>()
            where THypercube : HypercubeBase
        {
            return this.MemberwiseClone() as THypercube;
        }
    }

    public class Hypercube3D : HypercubeBase
    {
        public override int Dimensions => 3;

        public int Z { get; set; }

        public override int this[int dimension]
        {
            get
            {
                return dimension switch
                {
                    2 => this.Z,
                    _ => base[dimension],
                };
            }

            set
            {
                if (dimension == 2)
                {
                    this.Z = value;
                }
                else
                {
                    base[dimension] = value;
                }
            }
        }
    }

    public class Hypercube4D : Hypercube3D
    {
        public override int Dimensions => 4;

        public int W { get; set; }

        public override int this[int dimension]
        {
            get
            {
                return dimension switch
                {
                    3 => this.W,
                    _ => base[dimension],
                };
            }

            set
            {
                if (dimension == 3)
                {
                    this.W = value;
                }
                else
                {
                    base[dimension] = value;
                }
            }
        }
    }

    public class HypercubeSorter : IComparer<HypercubeBase>
    {
        public static HypercubeSorter Default { get; } = new HypercubeSorter();

        public int Compare(HypercubeBase x, HypercubeBase y)
        {
            if (x.Dimensions != y.Dimensions)
            {
                return x.Dimensions.CompareTo(y.Dimensions);
            }

            for (int d = 0; d < x.Dimensions; d++)
            {
                int comparison = x[d].CompareTo(y[d]);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return 0;
        }
    }
}
