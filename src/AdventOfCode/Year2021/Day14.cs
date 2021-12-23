// <copyright file="Day14.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day14 : DayBase<InsertionRule>
    {
        public override long Part1()
        {
            return this.RunPairInsertion(10);
        }

        public override long Part2()
        {
            return this.RunPairInsertion(40);
        }

        public long RunPairInsertion(int steps)
        {
            IDictionary<string, InsertionRule> rules = this.GetInput().ToDictionary(k => k.AdjacentPair);

            Dictionary<char, long> counts = new();
            foreach (char c in InsertionRule.Template)
            {
                if (!counts.ContainsKey(c))
                {
                    counts[c] = 0;
                }

                counts[c]++;
            }

            foreach (char c in rules.Values.Select(r => r.ToInsert))
            {
                if (!counts.ContainsKey(c))
                {
                    counts[c] = 0;
                }
            }

            Dictionary<string, long> pairs = new();
            for (int i = 1; i < InsertionRule.Template.Length; i++)
            {
                string pair = InsertionRule.Template.Substring(i - 1, 2);
                if (!pairs.ContainsKey(pair))
                {
                    pairs[pair] = 0;
                }

                pairs[pair]++;
            }

            for (int s = 0; s < steps; s++)
            {
                Dictionary<string, long> newPairs = new();

                foreach (KeyValuePair<string, long> pair in pairs)
                {
                    InsertionRule rule = rules[pair.Key];
                    counts[rule.ToInsert] += pair.Value;

                    string left = string.Concat(pair.Key[0], rule.ToInsert);
                    if (!newPairs.ContainsKey(left))
                    {
                        newPairs[left] = 0;
                    }

                    string right = string.Concat(rule.ToInsert, pair.Key[1]);
                    if (!newPairs.ContainsKey(right))
                    {
                        newPairs[right] = 0;
                    }

                    newPairs[left] += pair.Value;
                    newPairs[right] += pair.Value;
                }

                pairs = newPairs;
            }

            return counts.Max(p => p.Value) - counts.Min(p => p.Value);
        }
    }

    public class InsertionRule : IDayInput
    {
        public static string Template { get; set; }

        public string AdjacentPair { get; set; }

        public char ToInsert { get; set; }

        public void InitializeNewParse()
        {
            Template = null;
        }

        public bool Load(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            else if (!input.Contains(" -> "))
            {
                Template = input;
                return true;
            }
            else
            {
                this.AdjacentPair = input[..2];
                this.ToInsert = input[6];
                return false;
            }
        }
    }
}
