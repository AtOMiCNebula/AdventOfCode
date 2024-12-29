namespace NebulousIndustries.AdventOfCode.Year2024;

public class Day04 : DayBase<Day04Map>
{
    public override long Part1()
    {
        return this.GetInput().Single().CountMatchesForPart1();
    }

    public override long Part2()
    {
        return this.GetInput().Single().CountMatchesForPart2();
    }
}

public class Day04Map : IDayInput
{
    public List<string> Map { get; } = [];

    public long CountMatchesForPart1()
    {
        char[] pattern = ['X', 'M', 'A', 'S'];
        return CountMatches((y, x, dy, dx) =>
        {
            if (Map[y][x] != pattern[0])
            {
                return false;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                int yt = y + dy * i;
                int xt = x + dx * i;

                if (!(0 <= yt && yt < Map.Count) || !(0 <= xt && xt < Map[yt].Length) || Map[yt][xt] != pattern[i])
                {
                    return false;
                }
            }

            return true;
        });
    }

    public long CountMatchesForPart2()
    {
        char[] pattern = ['M', 'A', 'S'];
        return CountMatches((y, x, dy, dx) =>
        {
            if (Map[y][x] != pattern[1] || !((dy == 0) ^ (dx == 0)))
            {
                return false;
            }

            for (int i = -1; i <= 1; i += 2)
            {
                int yt1 = y + dy * i;
                int yt2 = yt1;
                int xt1 = x + dx * i;
                int xt2 = xt1;
                if (dx == 0)
                {
                    xt1 -= 1;
                    xt2 += 1;
                }
                else
                {
                    yt1 -= 1;
                    yt2 += 1;
                }

                if ((!(0 <= yt1 && yt1 < Map.Count) || !(0 <= xt1 && xt1 < Map[yt1].Length) || Map[yt1][xt1] != pattern[1 + i]) ||
                    (!(0 <= yt2 && yt2 < Map.Count) || !(0 <= xt2 && xt2 < Map[yt2].Length) || Map[yt2][xt2] != pattern[1 + i]))
                {
                    return false;
                }
            }

            return true;
        });
    }

    private long CountMatches(Func<int, int, int, int, bool> lambda)
    {
        long matches = 0;

        for (int y = 0; y < Map.Count; y++)
        {
            for (int x = 0; x < Map[y].Length; x++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        matches += lambda(y, x, dy, dx) ? 1 : 0;
                    }
                }
            }
        }

        return matches;
    }

    public bool Load(string input)
    {
        Map.Add(input);
        return true;
    }
}
