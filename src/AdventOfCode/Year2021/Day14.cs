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
            IDictionary<string, InsertionRule> rules = this.GetInput().ToDictionary(k => k.AdjacentPair);

            Dictionary<char, int> counts = new();
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

            LinkedList<char> chain = new(InsertionRule.Template);
            chain.AddLast('-');
            for (int i = 0; i < 10; i++)
            {
                for (LinkedListNode<char> node = chain.First.Next; node != chain.Last; node = node.Next)
                {
                    InsertionRule rule = rules[string.Concat(node.Previous.Value, node.Value)];
                    chain.AddBefore(node, rule.ToInsert);
                    counts[rule.ToInsert]++;
                }
            }

            return counts.Max(p => p.Value) - counts.Min(p => p.Value);
        }

        public override long Part2()
        {
            return -1;
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
