namespace NebulousIndustries.AdventOfCode.Year2021;

public class Day01 : DayBase
{
    public override long Part1()
    {
        IEnumerable<int> measurements = this.GetInputRaw().Select(i => int.Parse(i));
        return CountIncreases(measurements);
    }

    public override long Part2()
    {
        List<int> measurements = this.GetInputRaw().Select(i => int.Parse(i)).ToList();
        IEnumerable<int> slidingMeasurements = Enumerable.Range(2, measurements.Count - 2).Select(i => measurements[i - 2] + measurements[i - 1] + measurements[i]);
        return CountIncreases(slidingMeasurements);
    }

    protected static int CountIncreases(IEnumerable<int> measurements)
    {
        (int increases, _) = measurements.Skip(1).Aggregate((0, measurements.First()), (acc, measurement) =>
        {
            return (acc.Item1 + (acc.Item2 < measurement ? 1 : 0), measurement);
        });

        return increases;
    }
}
