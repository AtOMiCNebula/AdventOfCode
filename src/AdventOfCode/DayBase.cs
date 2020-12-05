using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NebulousIndustries.AdventOfCode
{
    public abstract class DayBase<T>
        where T : IDayInput, new()
    {
        public abstract int Number { get; }

        public IList<T> GetInput()
        {
            string[] lines = File.ReadAllLines($@"Year2020\Inputs\input{this.Number}.txt");
            return lines.Select(l =>
            {
                T t = new T();
                t.Load(l);
                return t;
            }).ToList();
        }

        public abstract void Part1();

        public abstract void Part2();
    }
}
