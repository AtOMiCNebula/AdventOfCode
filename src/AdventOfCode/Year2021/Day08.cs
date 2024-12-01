namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day08 : DayBase<SevenSegmentEntry>
    {
        public override long Part1()
        {
            return this.GetInput().Sum(e => e.ObviousOutputValueCount);
        }

        public override long Part2()
        {
            int result = 0;
            IEnumerable<SevenSegmentEntry> entries = this.GetInput();
            foreach (SevenSegmentEntry entry in entries)
            {
                SevenSegmentValue oneValue = entry.SignalPatterns.First(p => p.Value == 1);
                SevenSegmentValue fourValue = entry.SignalPatterns.First(p => p.Value == 4);

                int output = 0;
                foreach (SevenSegmentValue outputValue in entry.OutputValues)
                {
                    if (outputValue.Value == -1)
                    {
                        outputValue.ComputeValue(oneValue.SignalValue, fourValue.SignalValue);
                    }

                    output = (output * 10) + outputValue.Value;
                }

                result += output;
            }

            return result;
        }
    }

    public class SevenSegmentEntry : IDayInput
    {
        public IList<SevenSegmentValue> SignalPatterns { get; } = new List<SevenSegmentValue>();

        public IList<SevenSegmentValue> OutputValues { get; } = new List<SevenSegmentValue>();

        public int ObviousOutputValueCount => this.OutputValues.Count(o => o.IsObviousValue);

        public bool Load(string input)
        {
            string[] split = input.Split(" | ");
            foreach (string signalPattern in split[0].Split(" "))
            {
                this.SignalPatterns.Add(new SevenSegmentValue(signalPattern));
            }
            foreach (string outputValue in split[1].Split(" "))
            {
                this.OutputValues.Add(new SevenSegmentValue(outputValue));
            }

            return false;
        }
    }

    public class SevenSegmentValue
    {
        public SevenSegmentValue(string signalValue)
        {
            this.SignalValue = string.Join(string.Empty, signalValue.OrderBy(c => c));
            this.Value = this.SignalValue.Length switch
            {
                2 => 1,
                4 => 4,
                3 => 7,
                7 => 8,
                _ => -1,
            };
        }

        public string SignalValue { get; }

        public int Value { get; set; }

        public bool IsObviousValue => this.SignalValue.Length switch
        {
            2 or 4 or 3 or 7 => true, /* digits 1, 4, 7, 8 */
            _ => false,
        };

        public void ComputeValue(string oneSignalValue, string fourSignalValue)
        {
            int oneSegmentMatches = this.SignalValue.Count(c => oneSignalValue.Contains(c));
            int fourSegmentMatches = this.SignalValue.Count(c => fourSignalValue.Contains(c));
            if (oneSegmentMatches == 2 && fourSegmentMatches == 3)
            {
                // If this signal contains both segments a 1 has and three segments a 4 has, we must be
                // a 3 or a 0 based on length
                this.Value = this.SignalValue.Length == 5 ? 3 : 0;
            }
            else
            {
                if (this.SignalValue.Length == 6)
                {
                    // If this signal contains six segments, it will be either a 6 or a 9 depending on how
                    // many overlaps it shares with a 4
                    this.Value = fourSegmentMatches == 3 ? 6 : 9;
                }
                else
                {
                    // Similarly for five-segment signals, it will be either a 2 or a 5
                    this.Value = fourSegmentMatches == 2 ? 2 : 5;
                }
            }
        }
    }
}
