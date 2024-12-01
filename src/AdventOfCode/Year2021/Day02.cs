namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day02 : DayBase<SubmarineCommand>
    {
        public override long Part1()
        {
            IEnumerable<SubmarineCommand> commands = this.GetInput();
            (int depth, int distance) = commands.Aggregate((0, 0), (acc, command) =>
            {
                return command.Direction switch
                {
                    SubmarineCommand.SubmarineDirection.Forward => (acc.Item1, acc.Item2 + command.Distance),
                    SubmarineCommand.SubmarineDirection.Down => (acc.Item1 + command.Distance, acc.Item2),
                    SubmarineCommand.SubmarineDirection.Up => (acc.Item1 - command.Distance, acc.Item2),
                    _ => throw new InvalidOperationException(),
                };
            });
            return depth * distance;
        }

        public override long Part2()
        {
            IEnumerable<SubmarineCommand> commands = this.GetInput();
            (int depth, int distance, int aim) = commands.Aggregate((0, 0, 0), (acc, command) =>
            {
                return command.Direction switch
                {
                    SubmarineCommand.SubmarineDirection.Forward => (acc.Item1 + (acc.Item3 * command.Distance), acc.Item2 + command.Distance, acc.Item3),
                    SubmarineCommand.SubmarineDirection.Down => (acc.Item1, acc.Item2, acc.Item3 + command.Distance),
                    SubmarineCommand.SubmarineDirection.Up => (acc.Item1, acc.Item2, acc.Item3 - command.Distance),
                    _ => throw new InvalidOperationException(),
                };
            });
            return depth * distance;
        }
    }

    public class SubmarineCommand : IDayInput
    {
        public enum SubmarineDirection
        {
            Forward,
            Down,
            Up,
        }

        public SubmarineDirection Direction { get; set; }

        public int Distance { get; set; }

        public bool Load(string input)
        {
            string direction = input[..input.IndexOf(' ')];
            this.Direction = direction switch
            {
                "forward" => SubmarineDirection.Forward,
                "down" => SubmarineDirection.Down,
                "up" => SubmarineDirection.Up,
                _ => throw new InvalidOperationException($"Unknown direction '{direction}"),
            };

            this.Distance = int.Parse(input[(input.IndexOf(' ') + 1)..]);

            return false;
        }
    }
}
