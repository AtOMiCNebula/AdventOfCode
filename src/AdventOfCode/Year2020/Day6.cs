// <copyright file="Day6.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day6 : DayBase<CustomsFormLine>
    {
        public override int Number => 6;

        public override long Part1()
        {
            IEnumerable<CustomsFormLine> customsFormLines = this.GetInput();
            IEnumerable<CustomsForm> customsForms = ProcessCustomsForms(customsFormLines);
            return customsForms.Sum(f => f.QuestionsAnswered.Count);
        }

        public override long Part2()
        {
            IEnumerable<CustomsFormLine> customsFormLines = this.GetInput();
            IEnumerable<CustomsForm> customsForms = ProcessCustomsForms(customsFormLines);
            return customsForms.Sum(f => f.QuestionsAnswered.Where(q => q.Value == f.GroupMembers).Count());
        }

        public static IEnumerable<CustomsForm> ProcessCustomsForms(IEnumerable<CustomsFormLine> customsFormLines)
        {
            CustomsForm customsForm = new CustomsForm();
            List<CustomsForm> customsForms = new List<CustomsForm>() { customsForm };
            foreach (CustomsFormLine customsFormLine in customsFormLines)
            {
                int questionsRead = customsForm.ProcessLine(customsFormLine.Value);
                if (questionsRead == 0)
                {
                    customsForm = new CustomsForm();
                    customsForms.Add(customsForm);
                }
            }

            return customsForms;
        }
    }

    public class CustomsFormLine : IDayInput
    {
        public string Value { get; set; }

        public void Load(string input)
        {
            this.Value = input;
        }
    }

    public class CustomsForm
    {
        public int GroupMembers { get; set; }

        public Dictionary<char, int> QuestionsAnswered { get; } = new Dictionary<char, int>();

        public int ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return 0;
            }

            foreach (char questionAnswered in line)
            {
                if (!this.QuestionsAnswered.ContainsKey(questionAnswered))
                {
                    this.QuestionsAnswered.Add(questionAnswered, 0);
                }
                this.QuestionsAnswered[questionAnswered]++;
            }

            this.GroupMembers++;
            return line.Length;
        }
    }
}
