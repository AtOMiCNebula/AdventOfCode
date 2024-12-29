namespace NebulousIndustries.AdventOfCode.Year2024;

public class Day05 : DayBase<Day05Update>
{
    public override long Part1()
    {
        return this.GetInput().Where(u => u.IsValid()).Sum(u => u.MiddleIndex);
    }

    public override long Part2()
    {
        return this.GetInput().Where(u => !u.IsValid()).Select(u => u.Sort()).Sum(u => u.MiddleIndex);
    }
}

public class Day05Update : IDayInput
{
    private static bool ParsingRules { get; set; } = true;

    private static List<Tuple<int, int>> Rules { get; } = [];

    private List<int> Pages { get; } = [];

    public int MiddleIndex => Pages[Pages.Count / 2];

    public bool IsValid()
    {
        foreach (Tuple<int, int> rule in Rules)
        {
            int i1 = Pages.IndexOf(rule.Item1);
            int i2 = Pages.IndexOf(rule.Item2);
            if (i1 > -1 && i2 > -1 && i1 >= i2)
            {
                return false;
            }
        }

        return true;
    }

    public Day05Update Sort()
    {
        Day05Update newUpdate = new();
        newUpdate.Pages.AddRange(this.Pages);
        newUpdate.Pages.Sort((l, r) =>
        {
            if (Rules.Contains(Tuple.Create(l, r)))
            {
                return -1;
            }
            else if (Rules.Contains(Tuple.Create(r, l)))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });

        return newUpdate;
    }

    public void InitializeNewParse()
    {
        ParsingRules = true;
    }

    public bool Load(string input)
    {
        if (!ParsingRules)
        {
            IEnumerable<int> orders = input.Split(',').Select(int.Parse);
            Pages.AddRange(orders);
            return false;
        }
        else if (!string.IsNullOrEmpty(input))
        {
            IEnumerable<int> ruleSegments = input.Split('|').Select(int.Parse);
            Rules.Add(Tuple.Create(ruleSegments.First(), ruleSegments.Last()));
        }
        else
        {
            ParsingRules = false;
        }

        return true;
    }
}
