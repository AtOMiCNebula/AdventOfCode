using NebulousIndustries.AdventOfCode.Year2023;

namespace NebulousIndustries.AdventOfCode.Tests.Year2023;

[TestClass]
public class Day01Tests : DayTests<Day01>
{
    public override long Part1Answer => 55621;

    public override long Part2Answer => 53592;

    [DataTestMethod]
    [DataRow("1abc2", 12)]
    [DataRow("pqr3stu8vwx", 38)]
    [DataRow("a1b2c3d4e5f", 15)]
    [DataRow("treb7uchet", 77)]
    public void TestPart1Calculations(string input, int expected)
    {
        CalibrationValue value = new();
        Assert.IsFalse(value.Load(input));
        Assert.AreEqual(expected, value.Part1Value);
    }

    [DataTestMethod]
    [DataRow("two1nine", 29)]
    [DataRow("eightwothree", 83)]
    [DataRow("abcone2threexyz", 13)]
    [DataRow("xtwone3four", 24)]
    [DataRow("4nineeightseven2", 42)]
    [DataRow("zoneight234", 14)]
    [DataRow("7pqrstsixteen", 76)]
    public void TestPart2Calculations(string input, int expected)
    {
        CalibrationValue value = new();
        Assert.IsFalse(value.Load(input));
        Assert.AreEqual(expected, value.Part2Value);
    }
}
