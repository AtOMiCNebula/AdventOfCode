﻿// <copyright file="Day10.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Day10 : DayBase
    {
        public override int Number => 10;

        public override long Part1()
        {
            IEnumerable<int> adapters = this.GetAdapters();
            Dictionary<int, int> joltageDifferences = new Dictionary<int, int>();
            int lastAdapter = adapters.First();
            foreach (int adapter in adapters.Skip(1))
            {
                int joltageDifference = adapter - lastAdapter;
                if (!joltageDifferences.ContainsKey(joltageDifference))
                {
                    joltageDifferences.Add(joltageDifference, 0);
                }
                joltageDifferences[joltageDifference]++;
                lastAdapter = adapter;
            }

            Debug.Assert(joltageDifferences.Count <= 3, "Too many joltage differences");
            Debug.Assert(!joltageDifferences.Keys.Except(new[] { 1, 2, 3 }).Any(), "Unexpected joltage differences");
            return joltageDifferences[1] * joltageDifferences[3];
        }

        public override long Part2()
        {
            Dictionary<int, int> groups = new Dictionary<int, int>();
            IList<int> adapters = this.GetAdapters().ToList();

            int consecutive = 0;
            for (int i = 1; i < adapters.Count; i++)
            {
                if (adapters[i] - adapters[i - 1] == 1)
                {
                    consecutive++;
                }
                else if (consecutive > 0)
                {
                    if (!groups.ContainsKey(consecutive))
                    {
                        groups[consecutive] = 0;
                    }
                    groups[consecutive]++;
                    consecutive = 0;
                }
            }

            return (long)Math.Pow(2, groups[2]) * (long)Math.Pow(4, groups[3]) * (long)Math.Pow(7, groups[4]);
        }

        public IEnumerable<int> GetAdapters()
        {
            IEnumerable<int> adapters = this.GetInputRaw()
                .Select(i => int.Parse(i))
                .OrderBy(i => i);
            return adapters.Prepend(0).Append(adapters.Max() + 3);
        }
    }
}
