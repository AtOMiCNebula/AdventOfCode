using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulousIndustries.AdventOfCode.Year2020
{
    public class Day1 : DayBase<Expense>
    {
        public override int Number => 1;

        public override void Part1()
        {
            List<int> expenses = this.GetInput().Select(e => e.Value).ToList();
            for (int i = 0; i < expenses.Count; i++)
            {
                for (int j = i + 1; j < expenses.Count; j++)
                {
                    int sum = expenses[i] + expenses[j];
                    if (sum == 2020)
                    {
                        Console.WriteLine($"{expenses[i]} and {expenses[j]} sum to 2020, and multiply to {expenses[i] * expenses[j]}");
                    }
                }
            }
        }

        public override void Part2()
        {
            List<int> expenses = this.GetInput().Select(e => e.Value).ToList();
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
                        }
                    }
                }
            }
        }
    }

    public class Expense : IDayInput
    {
        public int Value { get; set; }

        public void Load(string input)
        {
            this.Value = int.Parse(input);
        }
    }
}
