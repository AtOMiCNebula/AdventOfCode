// <copyright file="Day6.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day6 : DayBase<CustomsForm>
    {
        public override int Number => 6;

        public override long Part1()
        {
            IEnumerable<CustomsForm> customsForms = this.GetInput();
            return customsForms.Sum(f => f.QuestionsAnswered.Count);
        }

        public override long Part2()
        {
            IEnumerable<CustomsForm> customsForms = this.GetInput();
            return customsForms.Sum(f => f.QuestionsAnswered.Where(q => q.Value == f.GroupMembers).Count());
        }
    }

    public class CustomsForm : IDayInput
    {
        public int GroupMembers { get; set; }

        public Dictionary<char, int> QuestionsAnswered { get; } = new Dictionary<char, int>();

        public bool Load(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (char questionAnswered in input)
            {
                if (!this.QuestionsAnswered.ContainsKey(questionAnswered))
                {
                    this.QuestionsAnswered.Add(questionAnswered, 0);
                }
                this.QuestionsAnswered[questionAnswered]++;
            }

            this.GroupMembers++;
            return true;
        }
    }
}
