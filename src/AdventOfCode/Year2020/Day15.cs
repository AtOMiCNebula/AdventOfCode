// <copyright file="Day15.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day15 : DayBase
    {
        public override int Number => 15;

        public override long Part1()
        {
            List<int> numbers = this.GetInputRaw().First().Split(',').Select(int.Parse).ToList();
            return GetSeries(numbers).Take(2020).Last();
        }

        public override long Part2()
        {
            return -1;
        }

        public static IEnumerable<int> GetSeries(IList<int> startingNumbers)
        {
            List<int> numbers = new List<int>(startingNumbers);
            for (int i = 0; i < numbers.Count; i++)
            {
                yield return numbers[i];
            }

            for (int i = numbers.Count; true; i++)
            {
                int next;
                int previous = numbers[i - 1];
                if (numbers.Count(num => num == previous) == 1)
                {
                    next = 0;
                }
                else
                {
                    int lastIndexOf = numbers.SkipLast(1).ToList().LastIndexOf(previous);
                    next = i - 1 - lastIndexOf;
                }

                numbers.Add(next);
                yield return next;
            }
        }
    }
}
