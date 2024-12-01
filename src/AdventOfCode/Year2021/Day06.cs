namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day06 : DayBase
    {
        public override long Part1()
        {
            return this.CountFishAfterDays(80);
        }

        public override long Part2()
        {
            return this.CountFishAfterDays(256);
        }

        protected long CountFishAfterDays(int days)
        {
            List<long> fishes = [0, 0, 0, 0, 0, 0, 0, 0, 0];
            foreach (int fish in this.GetInputRaw().Single().Split(",").Select(int.Parse))
            {
                fishes[fish]++;
            }

            for (int d = 0; d < days; d++)
            {
                fishes = [fishes[1], fishes[2], fishes[3], fishes[4], fishes[5], fishes[6], fishes[7] + fishes[0], fishes[8], fishes[0]];
            }

            return fishes.Sum();
        }
    }
}
