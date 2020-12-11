// <copyright file="Day10.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
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
            return -1;
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
