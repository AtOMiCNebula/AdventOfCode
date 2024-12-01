namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day01 : DayBase<Elf>
    {
        public override long Part1()
        {
            IEnumerable<Elf> elves = this.GetInput();
            return elves.Max(e => e.Food.Sum());
        }

        public override long Part2()
        {
            IEnumerable<Elf> elves = this.GetInput();
            return elves.Select(e => e.Food.Sum()).OrderByDescending(c => c).Take(3).Sum();
        }
    }

    public class Elf : IDayInput
    {
        public IList<int> Food { get; } = [];

        public bool Load(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int calories = int.Parse(input);
            this.Food.Add(calories);
            return true;
        }
    }
}
