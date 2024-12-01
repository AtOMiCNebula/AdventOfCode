namespace NebulousIndustries.AdventOfCode.Year2021;

public class Day07 : DayBase
{
    public override long Part1()
    {
        return this.CalculateMinimumFuel(true);
    }

    public override long Part2()
    {
        return this.CalculateMinimumFuel(false);
    }

    protected int CalculateMinimumFuel(bool linearFuel)
    {
        IEnumerable<int> crabs = this.GetInputRaw().Single().Split(",").Select(int.Parse);
        int maxPosition = crabs.Max();
        int minFuel = int.MaxValue;
        for (int i = 0; i <= maxPosition; i++)
        {
            int fuel = crabs.Sum(c => FuelForDistance(c, i, linearFuel));

            if (fuel < minFuel)
            {
                minFuel = fuel;
            }
        }

        return minFuel;
    }

    protected static int FuelForDistance(int c, int n, bool linearFuel)
    {
        int f = Math.Abs(c - n);
        if (!linearFuel)
        {
            f = ((f * f) + f) / 2;
        }

        return f;
    }
}
