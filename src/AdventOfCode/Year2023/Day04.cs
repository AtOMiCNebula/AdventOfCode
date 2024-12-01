namespace NebulousIndustries.AdventOfCode.Year2023;

using System;
using System.Collections.Generic;
using System.Linq;

public class Day04 : DayBase<ScratchCard>
{
    public override long Part1()
    {
        return this.GetInput()
            .Sum(s => s.PointValue);
    }

    public override long Part2()
    {
        List<ScratchCard> cards = this.GetInput().ToList();
        List<int> cardAmounts = new(Enumerable.Repeat(1, cards.Count));
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cards[i].Matches; j++)
            {
                cardAmounts[i + j + 1] += cardAmounts[i];
            }
        }
        return cardAmounts.Sum();
    }
}

public class ScratchCard : IDayInput
{
    public List<int> WinningNumbers { get; } = [];

    public List<int> Numbers { get; } = [];

    public int Matches => this.Numbers.Intersect(this.WinningNumbers).Count();

    public int PointValue => this.Matches > 0 ? (int)Math.Pow(2, this.Matches - 1) : 0;

    public bool Load(string input)
    {
        string[] inputs = input.Split([":", "|"], StringSplitOptions.TrimEntries);
        this.WinningNumbers.AddRange(inputs[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
        this.Numbers.AddRange(inputs[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
        return false;
    }
}
