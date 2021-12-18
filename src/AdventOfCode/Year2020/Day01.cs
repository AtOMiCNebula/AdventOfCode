// <copyright file="Day01.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day01 : DayBase
    {
        public override long Part1()
        {
            List<int> expenses = this.GetInputRaw().Select(i => int.Parse(i)).ToList();
            for (int i = 0; i < expenses.Count; i++)
            {
                for (int j = i + 1; j < expenses.Count; j++)
                {
                    int sum = expenses[i] + expenses[j];
                    if (sum == 2020)
                    {
                        Console.WriteLine($"{expenses[i]} and {expenses[j]} sum to 2020, and multiply to {expenses[i] * expenses[j]}");
                        return expenses[i] * expenses[j];
                    }
                }
            }

            return -1;
        }

        public override long Part2()
        {
            List<int> expenses = this.GetInputRaw().Select(i => int.Parse(i)).ToList();
            for (int i = 0; i < expenses.Count; i++)
            {
                for (int j = i + 1; j < expenses.Count; j++)
                {
                    for (int k = j + 1; k < expenses.Count; k++)
                    {
                        int sum = expenses[i] + expenses[j] + expenses[k];
                        if (sum == 2020)
                        {
                            Console.WriteLine($"{expenses[i]}, {expenses[j]}, and {expenses[k]} sum to 2020, and multiply to {expenses[i] * expenses[j] * expenses[k]}");
                            return expenses[i] * expenses[j] * expenses[k];
                        }
                    }
                }
            }

            return -1;
        }
    }
}
