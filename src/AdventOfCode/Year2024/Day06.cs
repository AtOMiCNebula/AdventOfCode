using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace NebulousIndustries.AdventOfCode.Year2024;

public class Day06 : DayBase<Day06Map>
{
    public override long Part1()
    {
        return this.GetInput().Single().Walk();
    }

    public override long Part2()
    {
        return this.GetInput().Single().WalkWithNewObstructions();
    }
}

public class Day06Map : IDayInput
{
    private List<MapFlags[]> Map { get; } = [];

    private int startY;
    private int startX;

    private enum Direction
    {
        North,
        East,
        South,
        West,
    }

    [Flags]
    private enum MapFlags
    {
        Occupied            = 0b000001,
        Visited             = 0b000010,
        VisitedGoingNorth   = 0b000110,
        VisitedGoingEast    = 0b001010,
        VisitedGoingSouth   = 0b010010,
        VisitedGoingWest    = 0b100010,
    }

    public Day06Map()
    {
    }

    public Day06Map(Day06Map other)
    {
        Map.AddRange(other.Map.Select(mf => (MapFlags[])mf.Clone()));
        startY = other.startY;
        startX = other.startX;
    }

    public int Walk()
    {
        int y = startY;
        int x = startX;
        Direction direction = Direction.North;

        while (y >= 0 && x >= 0)
        {
            (y, x, direction) = WalkOne(y, x, direction);
        }

        return Map.SelectMany(r => r).Count(c => c.HasFlag(MapFlags.Visited));
    }

    public int WalkWithNewObstructions()
    {
        Day06Map mapInitial = new(this);

        int y = startY;
        int x = startX;
        Direction direction = Direction.North;
        HashSet<(int, int)> cyclesFound = [];

        while (y >= 0 && x >= 0)
        {
            Day06Map mapWithNewObstruction = new(mapInitial);
            (int nexty, int nextx) = GetNextCoords(y, x, direction);
            if (0 <= nexty && nexty < mapWithNewObstruction.Map.Count && 0 <= nextx && nextx < mapWithNewObstruction.Map[nexty].Length && !mapWithNewObstruction.Map[nexty][nextx].HasFlag(MapFlags.Occupied) && !cyclesFound.Contains((nexty, nextx)))
            {
                mapWithNewObstruction.Map[nexty][nextx] |= MapFlags.Occupied;

                int innerY = startY;
                int innerX = startX;
                Direction innerDirection = Direction.North;

                while (innerY >= 0 && innerX >= 0)
                {
                    (innerY, innerX, innerDirection) = mapWithNewObstruction.WalkOne(innerY, innerX, innerDirection);
                }
                
                if (innerY == -2 && innerX == -2)
                {
                    mapWithNewObstruction.DisplayMap(nexty, nextx);
                    cyclesFound.Add((nexty, nextx));
                }
            }

            (y, x, direction) = WalkOne(y, x, direction);
        }

        return cyclesFound.Count;
    }

    private (int nexty, int nextx, Direction newdirection) WalkOne(int y, int x, Direction direction)
    {
        MapFlags newFlag = direction switch
        {
            Direction.North => MapFlags.VisitedGoingNorth,
            Direction.East => MapFlags.VisitedGoingEast,
            Direction.South => MapFlags.VisitedGoingSouth,
            Direction.West => MapFlags.VisitedGoingWest,
            _ => throw new UnreachableException(),
        };
        if (Map[y][x].HasFlag(newFlag))
        {
            return (-2, -2, Direction.North);
        }
        Map[y][x] |= newFlag;

        (int nexty, int nextx) = GetNextCoords(y, x, direction);
        if (0 <= nexty && nexty < Map.Count && 0 <= nextx && nextx < Map[nexty].Length)
        {
            if (Map[nexty][nextx].HasFlag(MapFlags.Occupied))
            {
                Direction nextdirection = direction switch
                {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    Direction.West => Direction.North,
                    _ => throw new UnreachableException(),
                };
                return (y, x, nextdirection);
            }

            return (nexty, nextx, direction);
        }
        else
        {
            return (-1, -1, Direction.North);
        }
    }

    private static (int nexty, int nextx) GetNextCoords(int y, int x, Direction direction)
    {
        int nexty = direction switch
        {
            Direction.North => y - 1,
            Direction.South => y + 1,
            _ => y,
        };
        int nextx = direction switch
        {
            Direction.West => x - 1,
            Direction.East => x + 1,
            _ => x,
        };
        return (nexty, nextx);
    }

    private void DisplayMap(int obstructionY, int obstructionX)
    {
        bool display = false;
        if (!display)
        {
            return;
        }

        Console.WriteLine($"Found cycle when adding obstruction at y({obstructionY}),x({obstructionX})");
        for (int dy = 0; dy < Map.Count; dy++)
        {
            for (int dx = 0; dx < Map[dy].Length; dx++)
            {
                MapFlags flag = Map[dy][dx];
                bool goingNorthSouth = flag.HasFlag(MapFlags.VisitedGoingNorth) || flag.HasFlag(MapFlags.VisitedGoingSouth);
                bool goingEastWest = flag.HasFlag(MapFlags.VisitedGoingEast) || flag.HasFlag(MapFlags.VisitedGoingWest);
                if (dy == startY && dx == startX)
                {
                    Console.Write('^');
                }
                else if (goingNorthSouth && goingEastWest)
                {
                    Console.Write('+');
                }
                else if (goingNorthSouth)
                {
                    Console.Write('|');
                }
                else if (goingEastWest)
                {
                    Console.Write('-');
                }
                else if (flag.HasFlag(MapFlags.Occupied))
                {
                    if (dy == obstructionY && dx == obstructionX)
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write('#');
                    }
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool Load(string input)
    {
        int startX = input.IndexOf('^');
        if (startX > -1)
        {
            this.startY = Map.Count;
            this.startX = startX;
        }

        Map.Add(input.Select(c => c == '#' ? MapFlags.Occupied : 0).ToArray());

        return true;
    }
}

