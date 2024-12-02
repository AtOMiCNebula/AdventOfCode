namespace NebulousIndustries.AdventOfCode.Year2024;

public class Day02 : DayBase<ReactorLevels>
{
    public override long Part1()
    {
        return this.GetInput().Count(r => r.AreLevelsSafe(0));
    }

    public override long Part2()
    {
        return this.GetInput().Count(r => r.AreLevelsSafe(1));
    }
}

public class ReactorLevels : IDayInput
{
    public List<int> Levels { get; } = [];

    public bool AreLevelsSafe(int allowableProblems)
    {
        int increasingCount = 0;
        for (int i = 1; i < this.Levels.Count; i++)
        {
            if (this.Levels[i - 1] < this.Levels[i])
            {
                increasingCount++;
            }
        }

        bool isIncreasing = increasingCount > 1;
        for (int i = 1; i < this.Levels.Count; i++)
        {
            bool stillIncreasing = isIncreasing && this.Levels[i - 1] <= this.Levels[i];
            bool stillDecreasing = !isIncreasing && this.Levels[i - 1] >= this.Levels[i];
            bool withinRange = Math.Abs(this.Levels[i - 1] - this.Levels[i]) is (> 0 and < 4);
            if (!(stillIncreasing || stillDecreasing) || !withinRange)
            {
                bool safeWithLeftRemoved = false;
                bool safeWithRightRemoved = false;
                if (allowableProblems > 0)
                {
                    ReactorLevels leftRemoved = new();
                    leftRemoved.Levels.AddRange(this.Levels.Where((l, idx) => idx != i - 1));
                    safeWithLeftRemoved = leftRemoved.AreLevelsSafe(allowableProblems - 1);

                    ReactorLevels rightRemoved = new();
                    rightRemoved.Levels.AddRange(this.Levels.Where((l, idx) => idx != i));
                    safeWithRightRemoved = rightRemoved.AreLevelsSafe(allowableProblems - 1);
                }

                return safeWithLeftRemoved || safeWithRightRemoved;
            }
        }

        return true;
    }

    public bool Load(string input)
    {
        this.Levels.AddRange(input.Split(' ').Select(int.Parse));
        return false;
    }
}
