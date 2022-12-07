// <copyright file="Day06.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day06 : DayBase
    {
        public override long Part1()
        {
            string input = this.GetInputRaw().Single();
            return Day06.FindMarker(input, 4);
        }

        public override long Part2()
        {
            string input = this.GetInputRaw().Single();
            return Day06.FindMarker(input, 14);
        }

        public static int FindMarker(string input, int uniques)
        {
            for (int i = 0; i < input.Length - uniques + 1; i++)
            {
                HashSet<char> chars = new();
                chars.Add(input[i]);

                bool shouldContinue = false;
                for (int j = 1; j < uniques; j++)
                {
                    if (!chars.Add(input[i + j]))
                    {
                        shouldContinue = true;
                        break;
                    }
                }

                if (shouldContinue)
                {
                    continue;
                }

                return i + uniques;
            }
            return -1;
        }
    }
}
