namespace NebulousIndustries.AdventOfCode;

public abstract class DayBase : DayBase<StringInput>
{
}

public abstract class DayBase<T> : DayBase<T, long>
    where T : IDayInput, new()
{
}

public abstract class DayBase<T, TResult> : IDay
    where T : IDayInput, new()
{
    public int Year => int.Parse(this.GetType().Namespace[^4..]);

    public int Number => int.Parse(this.GetType().Name[^2..]);

    public string InputFileVariant { get; set; } = string.Empty;

    public IEnumerable<string> GetInputRaw()
    {
        return File.ReadAllLines($@"Year{this.Year}\Inputs\input{this.Number:D2}{this.InputFileVariant}.txt");
    }

    public IEnumerable<T> GetInput()
    {
        bool inserted = false;
        T input = new();
        input.InitializeNewParse();
        List<T> inputs = [];
        foreach (string line in this.GetInputRaw())
        {
            if (!inserted)
            {
                inputs.Add(input);
                inserted = true;
            }

            bool keepAggregating = input.Load(line);
            if (!keepAggregating)
            {
                input = new T();
                inserted = false;
            }
        }

        return inputs;
    }

    public abstract TResult Part1();

    public abstract TResult Part2();

    object IDay.Part1()
    {
        return this.Part1();
    }

    object IDay.Part2()
    {
        return this.Part2();
    }
}

public class StringInput : IDayInput
{
    public string Value { get; set; }

    public bool Load(string input)
    {
        this.Value = input;
        return false;
    }
}
