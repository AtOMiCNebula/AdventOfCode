// <copyright file="Day09.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day09 : DayBase
    {
        public static int PreambleLength { get; } = 25;

        public override long Part1()
        {
            IList<long> numbers = this.GetInputRaw().Select(s => long.Parse(s)).ToList();
            for (int i = PreambleLength; i < numbers.Count; i++)
            {
                bool foundSum = false;
                for (int j = i - PreambleLength; j < i && !foundSum; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        long sum = numbers[j] + numbers[k];
                        if (sum == numbers[i])
                        {
                            Console.WriteLine($"Found matching pair for numbers[{i}]: {numbers[j]} + {numbers[k]}");
                            foundSum = true;
                            break;
                        }
                    }
                }

                if (!foundSum)
                {
                    return numbers[i];
                }
            }

            return -1;
        }

        public override long Part2()
        {
            long invalidNumber = this.Part1();
            IList<long> numbers = this.GetInputRaw().Select(s => long.Parse(s)).ToList();

            for (int i = PreambleLength; i < numbers.Count; i++)
            {
                long cumulativeSum = numbers[i];
                long smallest = numbers[i];
                long largest = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    cumulativeSum += numbers[j];
                    smallest = Math.Min(smallest, numbers[j]);
                    largest = Math.Max(largest, numbers[j]);
                    if (cumulativeSum == invalidNumber)
                    {
                        return smallest + largest;
                    }
                    else if (cumulativeSum > invalidNumber)
                    {
                        break;
                    }
                }
            }

            return -1;
        }
    }
}
