namespace NebulousIndustries.AdventOfCode.Year2022;

public class Day03 : DayBase<Rucksack>
{
    public override long Part1()
    {
        IEnumerable<Rucksack> rucksacks = this.GetInput();
        return rucksacks
            .Select(r => r.FindDuplicate())
            .Sum(Rucksack.ComputePriority);
    }

    public override long Part2()
    {
        IEnumerable<Rucksack> rucksacks = this.GetInput();
        return rucksacks
            .Chunk(3)
            .Select(r => r[0].FindBadge(r[1], r[2]))
            .Sum(Rucksack.ComputePriority);
    }
}

public class Rucksack : IDayInput
{
    public string Compartment1 { get; set; }

    public string Compartment2 { get; set; }

    public string AllItems => string.Concat(this.Compartment1, this.Compartment2);

    public bool Load(string input)
    {
        int middle = input.Length / 2;
        this.Compartment1 = input[..middle];
        this.Compartment2 = input[middle..];
        return false;
    }

    public char FindDuplicate()
    {
        return this.Compartment1
            .Intersect(this.Compartment2)
            .Single();
    }

    public char FindBadge(Rucksack second, Rucksack third)
    {
        return this.AllItems
            .Intersect(second.AllItems)
            .Intersect(third.AllItems)
            .Single();
    }

    public static int ComputePriority(char c)
    {
        return char.IsLower(c)
            ? (c - 'a' + 1)
            : (c - 'A' + 27);
    }
}
