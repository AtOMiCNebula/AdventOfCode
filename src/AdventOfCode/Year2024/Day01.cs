namespace NebulousIndustries.AdventOfCode.Year2024;

using System;
using System.Collections.Generic;
using System.Linq;

public class Day01 : DayBase<NumberPair>
{
    public override long Part1()
    {
        IEnumerable<int> left = this.GetInput().Select(p => p.Left).OrderBy(n => n);
        IEnumerable<int> right = this.GetInput().Select(p => p.Right).OrderBy(n => n);
        IEnumerable<int> distances = left.Zip(right).Select(p => Math.Abs(p.First - p.Second));
        return distances.Sum();
    }

    public override long Part2()
    {
        IEnumerable<int> left = this.GetInput().Select(p => p.Left);
        IEnumerable<int> right = this.GetInput().Select(p => p.Right);
        return left.Select(l => l * right.Count(r => r == l)).Sum();
    }
}

public class NumberPair : IDayInput
{
    public int Left { get; set; }

    public int Right { get; set; }

    public bool Load(string input)
    {
        string[] numbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        this.Left = int.Parse(numbers[0]);
        this.Right = int.Parse(numbers[1]);
        return false;
    }
}
