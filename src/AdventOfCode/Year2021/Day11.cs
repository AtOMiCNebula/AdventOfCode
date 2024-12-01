namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day11 : DayBase
    {
        public override long Part1()
        {
            int[][] octopi = this.GetInputRaw().Select(s => s.ToCharArray().Select(c => (int)c - '0').ToArray()).ToArray();

            int flashes = 0;
            for (int s = 0; s < 100; s++)
            {
                flashes += Step(octopi);
            }

            return flashes;
        }

        public override long Part2()
        {
            int[][] octopi = this.GetInputRaw().Select(s => s.ToCharArray().Select(c => (int)c - '0').ToArray()).ToArray();

            for (int s = 0; ; s++)
            {
                int flashes = Step(octopi);
                if (flashes == octopi.Length * octopi[0].Length)
                {
                    return s + 1;
                }
            }
        }

        protected static int Step(int[][] octopi)
        {
            int flashes = 0;

            // Increase energy levels
            Queue<(int Y, int X)> flashers = new();
            for (int y = 0; y < octopi.Length; y++)
            {
                for (int x = 0; x < octopi[y].Length; x++)
                {
                    IncreaseEnergy(octopi, y, x, flashers);
                }
            }

            // Handle flashes (and potential cascading)
            Queue<(int Y, int X)> highEnergyOctopi = new();
            while (flashers.Count > 0)
            {
                (int yFlash, int xFlash) = flashers.Dequeue();
                highEnergyOctopi.Enqueue((yFlash, xFlash));
                flashes++;

                for (int yDelta = -1; yDelta <= 1; yDelta++)
                {
                    int y = yFlash + yDelta;
                    if (0 > y || y >= octopi.Length)
                    {
                        continue;
                    }

                    for (int xDelta = -1; xDelta <= 1; xDelta++)
                    {
                        int x = xFlash + xDelta;
                        if (0 > x || x >= octopi[y].Length)
                        {
                            continue;
                        }

                        IncreaseEnergy(octopi, y, x, flashers);
                    }
                }
            }

            // Decrease high-energy octopi
            while (highEnergyOctopi.Count > 0)
            {
                (int y, int x) = highEnergyOctopi.Dequeue();
                octopi[y][x] = 0;
            }

            return flashes;
        }

        protected static void IncreaseEnergy(int[][] octopi, int y, int x, Queue<(int Y, int X)> flashers)
        {
            octopi[y][x]++;
            if (octopi[y][x] == 10)
            {
                flashers.Enqueue((y, x));
            }
        }
    }
}
