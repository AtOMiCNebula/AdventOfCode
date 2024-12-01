namespace NebulousIndustries.AdventOfCode.Year2021;

public class Day09 : DayBase
{
    public override long Part1()
    {
        List<string> heights = this.GetInputRaw().ToList();
        IList<(int Y, int X)> minimums = FindMinimums(heights);

        int score = 0;
        foreach ((int y, int x) in minimums)
        {
            score += heights[y][x] - '0' + 1;
        }

        return score;
    }

    public override long Part2()
    {
        List<string> heights = this.GetInputRaw().ToList();
        IList<(int Y, int X)> minimums = FindMinimums(heights);

        List<int> basins = [];
        foreach ((int y, int x) in minimums)
        {
            int basin = MeasureBasin(heights, y, x, new HashSet<(int Y, int X)>());
            basins.Add(basin);
        }

        return basins.OrderByDescending(b => b).Take(3).Aggregate(1, (acc, i) => acc * i);
    }

    protected static IList<(int Y, int X)> FindMinimums(IList<string> heights)
    {
        List<(int Y, int X)> minimums = [];
        for (int y = 0; y < heights.Count; y++)
        {
            for (int x = 0; x < heights[y].Length; x++)
            {
                char current = heights[y][x];

                if (y > 0 && current >= heights[y - 1][x])
                {
                    continue;
                }
                else if (y < heights.Count - 1 && current >= heights[y + 1][x])
                {
                    continue;
                }
                else if (x > 0 && current >= heights[y][x - 1])
                {
                    continue;
                }
                else if (x < heights[y].Length - 1 && current >= heights[y][x + 1])
                {
                    continue;
                }
                else
                {
                    minimums.Add((y, x));
                }
            }
        }

        return minimums;
    }

    protected static int MeasureBasin(IList<string> heights, int y, int x, ISet<(int Y, int X)> members)
    {
        if (members.Contains((y, x)))
        {
            // We've already counted this coordinate
            return 0;
        }
        else if (heights[y][x] == '9')
        {
            // 9 is never a basin member
            return 0;
        }
        else if (!members.Contains((y, x)))
        {
            members.Add((y, x));
        }

        int score = 1;
        char current = heights[y][x];
        if (y > 0 && current < heights[y - 1][x])
        {
            score += MeasureBasin(heights, y - 1, x, members);
        }
        if (y < heights.Count - 1 && current < heights[y + 1][x])
        {
            score += MeasureBasin(heights, y + 1, x, members);
        }
        if (x > 0 && current < heights[y][x - 1])
        {
            score += MeasureBasin(heights, y, x - 1, members);
        }
        if (x < heights[y].Length - 1 && current < heights[y][x + 1])
        {
            score += MeasureBasin(heights, y, x + 1, members);
        }

        return score;
    }
}
