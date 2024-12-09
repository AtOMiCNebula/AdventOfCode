using System.Text.RegularExpressions;

namespace NebulousIndustries.AdventOfCode.Year2024;

public class Day03 : DayBase<Multiplications>
{
    public override long Part1()
    {
        return this.GetInput().Single().Tuples.Sum(t => t.Item1 * t.Item2);
    }

    public override long Part2()
    {
        Multiplications.ProcessDosAndDonts = true;
        return this.GetInput().Single().Tuples.Sum(t => t.Item1 * t.Item2);
    }
}

public class Multiplications : IDayInput
{
    public static bool ProcessDosAndDonts { get; set; }

    public List<Tuple<int, int>> Tuples { get; } = [];

    private bool Dont { get; set; }

    public Regex MulPattern { get; } = new(@"(mul\(([0-9]+),([0-9]+)\))|(do\(\))|(don't\(\))");

    public bool Load(string input)
    {
        for (Match match = MulPattern.Match(input); match.Success; match = MulPattern.Match(input, match.Index + 1))
        {
            if (match.Groups[5].Success)
            {
                Dont = true;
            }
            else if (match.Groups[4].Success)
            {
                Dont = false;
            }
            else if (!Dont || !ProcessDosAndDonts)
            {
                Tuples.Add(Tuple.Create(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
            }
        }

        return true;
    }
}
