namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day04 : DayBase<SectionAssignment>
    {
        public override long Part1()
        {
            IEnumerable<SectionAssignment> assignments = this.GetInput();
            return assignments.Count(a => a.FullyContained);
        }

        public override long Part2()
        {
            IEnumerable<SectionAssignment> assignments = this.GetInput();
            return assignments.Count(a => a.HasOverlap);
        }
    }

    public class SectionAssignment : IDayInput
    {
        public int Low1 { get; set; }

        public int High1 { get; set; }

        public int Low2 { get; set; }

        public int High2 { get; set; }

        public bool FullyContained
        {
            get
            {
                return
                    (this.Low1 <= this.Low2 && this.High2 <= this.High1) ||
                    (this.Low2 <= this.Low1 && this.High1 <= this.High2);
            }
        }

        public bool HasOverlap
        {
            get
            {
                return
                    (this.Low1 <= this.Low2 && this.Low2 <= this.High1) ||
                    (this.Low1 <= this.High2 && this.High2 <= this.High1) ||
                    (this.Low2 <= this.Low1 && this.Low1 <= this.High2) ||
                    (this.Low2 <= this.High1 && this.High1 <= this.High2);
            }
        }

        public bool Load(string input)
        {
            int[] numbers = input
                .Split('-', ',')
                .Select(int.Parse)
                .ToArray();
            this.Low1 = numbers[0];
            this.High1 = numbers[1];
            this.Low2 = numbers[2];
            this.High2 = numbers[3];
            return false;
        }
    }
}
