namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;

    public class Day12 : DayBase<NavigationInstruction>
    {
        public override long Part1()
        {
            List<NavigationInstruction.NavigationDirection> directionCalculator =
            [
                NavigationInstruction.NavigationDirection.North,
                NavigationInstruction.NavigationDirection.East,
                NavigationInstruction.NavigationDirection.South,
                NavigationInstruction.NavigationDirection.West,
            ];

            int northDistance = 0;
            int eastDistance = 0;
            NavigationInstruction.NavigationDirection currentDirection = NavigationInstruction.NavigationDirection.East;
            foreach (NavigationInstruction instruction in this.GetInput())
            {
                NavigationInstruction.NavigationDirection direction = instruction.Direction;
                if (direction == NavigationInstruction.NavigationDirection.Forward)
                {
                    direction = currentDirection;
                }

                switch (direction)
                {
                    case NavigationInstruction.NavigationDirection.North:
                        northDistance += instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.South:
                        northDistance -= instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.East:
                        eastDistance += instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.West:
                        eastDistance -= instruction.Amount;
                        break;

                    case NavigationInstruction.NavigationDirection.Left:
                        currentDirection = directionCalculator[(4 + directionCalculator.IndexOf(currentDirection) - (instruction.Amount / 90)) % 4];
                        break;
                    case NavigationInstruction.NavigationDirection.Right:
                        currentDirection = directionCalculator[(4 + directionCalculator.IndexOf(currentDirection) + (instruction.Amount / 90)) % 4];
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

            return Math.Abs(northDistance) + Math.Abs(eastDistance);
        }

        public override long Part2()
        {
            int northWaypoint = 1;
            int eastWaypoint = 10;
            int northDistance = 0;
            int eastDistance = 0;
            foreach (NavigationInstruction instruction in this.GetInput())
            {
                switch (instruction.Direction)
                {
                    case NavigationInstruction.NavigationDirection.North:
                        northWaypoint += instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.South:
                        northWaypoint -= instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.East:
                        eastWaypoint += instruction.Amount;
                        break;
                    case NavigationInstruction.NavigationDirection.West:
                        eastWaypoint -= instruction.Amount;
                        break;

                    case NavigationInstruction.NavigationDirection.Left:
                    case NavigationInstruction.NavigationDirection.Right:
                        int rotations = (instruction.Amount / 90) % 4;
                        bool left = instruction.Direction == NavigationInstruction.NavigationDirection.Left;
                        if ((left && rotations == 1) || (!left && rotations == 3))
                        {
                            // Left 90 degrees
                            int swap = northWaypoint;
                            northWaypoint = eastWaypoint;
                            eastWaypoint = -swap;
                        }
                        else if (rotations == 2)
                        {
                            // 180 degrees
                            northWaypoint *= -1;
                            eastWaypoint *= -1;
                        }
                        else if ((left && rotations == 3) || (!left && rotations == 1))
                        {
                            // Right 90 degrees
                            int swap = northWaypoint;
                            northWaypoint = -eastWaypoint;
                            eastWaypoint = swap;
                        }

                        break;

                    case NavigationInstruction.NavigationDirection.Forward:
                        northDistance += northWaypoint * instruction.Amount;
                        eastDistance += eastWaypoint * instruction.Amount;
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

            return Math.Abs(northDistance) + Math.Abs(eastDistance);
        }
    }

    public class NavigationInstruction : IDayInput
    {
        public enum NavigationDirection
        {
            North,
            South,
            East,
            West,
            Left,
            Right,
            Forward,
        }

        public NavigationDirection Direction { get; set; }

        public int Amount { get; set; }

        public bool Load(string input)
        {
            this.Direction = input[0] switch
            {
                'N' => NavigationDirection.North,
                'S' => NavigationDirection.South,
                'E' => NavigationDirection.East,
                'W' => NavigationDirection.West,
                'L' => NavigationDirection.Left,
                'R' => NavigationDirection.Right,
                'F' => NavigationDirection.Forward,
                _ => throw new NotSupportedException(),
            };
            this.Amount = int.Parse(input[1..]);

            return false;
        }
    }
}
