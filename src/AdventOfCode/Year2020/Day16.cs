// <copyright file="Day16.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day16 : DayBase<TrainTicket>
    {
        public override int Number => 16;

        public override long Part1()
        {
            int ticketScanningErrorRate = 0;
            foreach (TrainTicket ticket in this.GetInput())
            {
                foreach (int invalidValue in ticket.InvalidValues)
                {
                    ticketScanningErrorRate += invalidValue;
                }
            }

            return ticketScanningErrorRate;
        }

        public override long Part2()
        {
            long result = 1;
            IEnumerable<TrainTicket> validTickets = this.GetInput().Where(t => t.IsValid);
            List<TrainTicketRule> unassignedRules = new List<TrainTicketRule>(TrainTicket.Rules);
            List<TrainTicketRule> assignedRules = new List<TrainTicketRule>();
            do
            {
                (TrainTicketRule assignedRule, int assignedIndex) = FindRulePosition(validTickets, unassignedRules, TrainTicket.Rules.Count);
                Console.WriteLine($"Assigned rule \"{assignedRule.Name}\" to index {assignedIndex}");
                unassignedRules.Remove(assignedRule);
                assignedRules.Add(assignedRule);
                if (assignedRule.Name.StartsWith("departure "))
                {
                    result *= validTickets.First().Values[assignedIndex];
                }
            }
            while (unassignedRules.Count > 0);

            return result;
        }

        public static (TrainTicketRule Rule, int Index) FindRulePosition(IEnumerable<TrainTicket> validTickets, IEnumerable<TrainTicketRule> unassignedRules, int totalValues)
        {
            for (int i = 0; i < totalValues; i++)
            {
                IEnumerable<TrainTicketRule> potentialRules = unassignedRules.Where(r => !validTickets.Any(t => !r.Matches(t.Values[i])));
                if (potentialRules.Count() == 1)
                {
                    return (potentialRules.First(), i);
                }
            }

            throw new NotSupportedException();
        }
    }

    public class TrainTicket : IDayInput
    {
        public static IList<TrainTicketRule> Rules { get; } = new List<TrainTicketRule>();

        public IList<int> Values { get; } = new List<int>();

        public IEnumerable<int> InvalidValues
        {
            get
            {
                foreach (int value in this.Values)
                {
                    bool matchedRule = TrainTicket.Rules.Any(r => r.Matches(value));
                    if (!matchedRule)
                    {
                        yield return value;
                    }
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return !this.InvalidValues.Any();
            }
        }

        public void InitializeNewParse()
        {
            TrainTicket.Rules.Clear();
        }

        public bool Load(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input == "your ticket:" || input == "nearby tickets:")
            {
                return true;
            }
            else if (input.IndexOf(':') != -1)
            {
                // Parse a rule
                string[] ranges = input[(input.IndexOf(": ") + 2)..].Split(" or ");
                TrainTicket.Rules.Add(new TrainTicketRule
                {
                    Name = input[..input.IndexOf(':')],
                    Range1Lower = int.Parse(ranges[0][..ranges[0].IndexOf('-')]),
                    Range1Upper = int.Parse(ranges[0][(ranges[0].IndexOf('-') + 1)..]),
                    Range2Lower = int.Parse(ranges[1][..ranges[1].IndexOf('-')]),
                    Range2Upper = int.Parse(ranges[1][(ranges[1].IndexOf('-') + 1)..]),
                });

                return true;
            }
            else
            {
                foreach (int value in input.Split(',').Select(int.Parse))
                {
                    this.Values.Add(value);
                }

                return false;
            }
        }
    }

    public class TrainTicketRule
    {
        public string Name { get; set; }

        public int Range1Lower { get; set; }

        public int Range1Upper { get; set; }

        public int Range2Lower { get; set; }

        public int Range2Upper { get; set; }

        public bool Matches(int value)
        {
            return (this.Range1Lower <= value && value <= this.Range1Upper)
                    || (this.Range2Lower <= value && value <= this.Range2Upper);
        }
    }
}
