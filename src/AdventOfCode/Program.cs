using System;

using NebulousIndustries.AdventOfCode.Year2020;

[assembly: CLSCompliant(true)]
namespace NebulousIndustries.AdventOfCode
{
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
