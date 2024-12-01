namespace NebulousIndustries.AdventOfCode.Year2023;

public class Day01 : DayBase<CalibrationValue>
{
    public override long Part1()
    {
        return this.GetInput().Sum(cv => cv.Part1Value);
    }

    public override long Part2()
    {
        return this.GetInput().Sum(cv => cv.Part2Value);
    }
}

public class CalibrationValue : IDayInput
{
    public int Part1Value => GetValue(this.Input);

    public int Part2Value => GetValue(this.Input
            .Replace("one", "one1one")
            .Replace("two", "two2two")
            .Replace("three", "three3three")
            .Replace("four", "four4four")
            .Replace("five", "five5five")
            .Replace("six", "six6six")
            .Replace("seven", "seven7seven")
            .Replace("eight", "eight8eight")
            .Replace("nine", "nine9nine"));

    private string Input { get; set; }

    public bool Load(string input)
    {
        this.Input = input;
        return false;
    }

    private static int GetValue(string input)
    {
        int firstDigit = input.First(char.IsDigit) - '0';
        int lastDigit = input.Last(char.IsDigit) - '0';
        return (firstDigit * 10) + lastDigit;
    }
}
