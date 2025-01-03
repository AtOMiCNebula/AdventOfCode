﻿namespace NebulousIndustries.AdventOfCode.Year2020;

public class Day15 : DayBase
{
    public override long Part1()
    {
        List<int> numbers = this.GetInputRaw().First().Split(',').Select(int.Parse).ToList();
        return GetResult(numbers, 2020);
    }

    public override long Part2()
    {
        List<int> numbers = this.GetInputRaw().First().Split(',').Select(int.Parse).ToList();
        return GetResult(numbers, 30000000);
    }

    public static long GetResult(IList<int> startingNumbers, int num)
    {
        int previous = -1;
        Dictionary<int, (int MostRecent, int Previous)> history = [];
        for (int i = 0; i < num; i++)
        {
            int next;
            if (i < startingNumbers.Count)
            {
                next = startingNumbers[i];
            }
            else if (history[previous].Previous == -1)
            {
                next = 0;
            }
            else
            {
                next = history[previous].MostRecent - history[previous].Previous;
            }

            history[next] = (i, history.TryGetValue(next, out (int MostRecent, int Previous) value) ? value.MostRecent : -1);
            previous = next;
        }

        return previous;
    }
}
