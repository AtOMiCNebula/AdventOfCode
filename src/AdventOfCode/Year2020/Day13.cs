namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day13 : DayBase
    {
        public override long Part1()
        {
            IEnumerable<string> input = this.GetInputRaw();
            int earliestDeparture = int.Parse(input.First());
            IEnumerable<int> routes = input.Last().Split(',').Where(s => s != "x").Select(int.Parse);
            int earliestRoute = int.MaxValue;
            int earliestRouteDeparture = int.MaxValue;
            foreach (int route in routes)
            {
                int firstAvailableDeparture = route * (int)Math.Ceiling((double)earliestDeparture / route);
                if (firstAvailableDeparture < earliestRouteDeparture)
                {
                    earliestRoute = route;
                    earliestRouteDeparture = firstAvailableDeparture;
                }
            }

            return earliestRoute * (earliestRouteDeparture - earliestDeparture);
        }

        public override long Part2()
        {
            return -1;
        }
    }
}
