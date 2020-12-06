// <copyright file="Program.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

[assembly: System.CLSCompliant(true)]

namespace NebulousIndustries.AdventOfCode
{
    using NebulousIndustries.AdventOfCode.Year2020;

    public static class Program
    {
        public static void Main()
        {
            var day = new Day5();
            day.Part1();
            day.Part2();
        }
    }
}
