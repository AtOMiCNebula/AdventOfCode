namespace NebulousIndustries.AdventOfCode.Year2023;

public class Day02 : DayBase<CubeGameResult>
{
    public override long Part1()
    {
        return this
            .GetInput()
            .Where(g => g.Rolls.All(r =>
                r.Red <= 12 &&
                r.Green <= 13 &&
                r.Blue <= 14))
            .Sum(g => g.GameID);
    }

    public override long Part2()
    {
        return this
            .GetInput()
            .Select(g =>
                g.Rolls.Max(r => r.Red) *
                g.Rolls.Max(r => r.Green) *
                g.Rolls.Max(r => r.Blue))
            .Sum();
    }
}

public class CubeGameResult : IDayInput
{
    public int GameID { get; set; }

    public IList<(int Red, int Green, int Blue)> Rolls { get; } = [];

    public bool Load(string input)
    {
        this.GameID = int.Parse(input.Split(':')[0].Substring(5));

        input = input.Substring(input.IndexOf(": ") + 2);
        foreach (string round in input.Split(';', StringSplitOptions.TrimEntries))
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            foreach (string roll in round.Split(',', StringSplitOptions.TrimEntries))
            {
                string[] rollSplit = roll.Split(' ');
                switch (rollSplit[1])
                {
                    case "red":
                        red = int.Parse(rollSplit[0]);
                        break;
                    case "green":
                        green = int.Parse(rollSplit[0]);
                        break;
                    case "blue":
                        blue = int.Parse(rollSplit[0]);
                        break;
                }
            }

            this.Rolls.Add((red, green, blue));
        }

        return false;
    }
}
