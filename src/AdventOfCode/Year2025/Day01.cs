namespace NebulousIndustries.AdventOfCode.Year2025;

public class Day01 : DayBase<Day01DialInstruction>
{
    public override long Part1()
    {
        IEnumerable<Day01DialInstruction> instructions = this.GetInput();

        int zeros = 0;
        int pos = 50;
        foreach (Day01DialInstruction instruction in instructions)
        {
            pos += (instruction.TurnLeft ? -1 : 1) * instruction.Amount;
            pos %= 100;

            if (pos == 0)
            {
                zeros++;
            }
        }

        return zeros;
    }

    public override long Part2()
    {
        return CountZeros(false);
    }

    private long CountZeros(bool directHitsOnly)
    {
        IEnumerable<Day01DialInstruction> instructions = this.GetInput();

        int zeros = 0;
        int pos = 50;
        foreach (Day01DialInstruction instruction in instructions)
        {
            bool startedAtZero = pos == 0;
            pos += (instruction.TurnLeft ? -1 : 1) * instruction.Amount;

            if (pos == 0)
            {
                zeros++;
                Console.WriteLine("\t+1 (direct)");
            }
            else if (pos < 0)
            {
                while (pos < 0)
                {
                    pos += 100;
                    if (!startedAtZero)
                    {
                        if (!directHitsOnly)
                        {
                            zeros++;
                            Console.WriteLine("\t+1 (too low)");
                        }
                    }
                    else
                    {
                        startedAtZero = false;
                    }
                }

                if (pos == 0)
                {
                    zeros++;
                    Console.WriteLine("\t+1 (too low direct)");
                }
            }
            else if (pos >= 100)
            {
                while (pos >= 100)
                {
                    pos -= 100;

                    if (!directHitsOnly)
                    {
                        zeros++;
                        Console.WriteLine("\t+1 (too high)");
                    }
                }
            }

            Console.WriteLine(pos);
        }

        return zeros;
    }
}

public class Day01DialInstruction : IDayInput
{
    public bool TurnLeft { get; private set; }

    public int Amount { get; private set; }

    public bool Load(string input)
    {
        this.TurnLeft = input[0] == 'L';
        this.Amount = int.Parse(input[1..]);
        return false;
    }
}
