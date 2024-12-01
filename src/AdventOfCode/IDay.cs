namespace NebulousIndustries.AdventOfCode
{
    public interface IDay
    {
        public int Year { get; }

        public int Number { get; }

        public object Part1();

        public object Part2();
    }
}
