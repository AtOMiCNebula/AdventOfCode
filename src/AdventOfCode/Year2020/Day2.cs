using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulousIndustries.AdventOfCode.Year2020
{
    public class Day2 : DayBase<Password>
    {
        public override int Number => 2;

        public override void Part1()
        {
            IList<Password> passwords = this.GetInput();
            Console.WriteLine($"{passwords.Where(p => p.IsValidForPart1()).Count()}");
        }

        public override void Part2()
        {
            IList<Password> passwords = this.GetInput();
            Console.WriteLine($"{passwords.Where(p => p.IsValidForPart2()).Count()}");
        }
    }

    public class Password : IDayInput
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }

        public char RequiredCharacter { get; set; }

        public string Value { get; set; }

        public bool IsValidForPart1()
        {
            int count = this.Value.Where(c => c == this.RequiredCharacter).Count();
            return this.FirstNumber <= count && count <= this.SecondNumber;
        }

        public bool IsValidForPart2()
        {
            bool containsFirst = this.Value.Length >= this.FirstNumber && this.Value[this.FirstNumber - 1] == this.RequiredCharacter;
            bool containsSecond = this.Value.Length >= this.SecondNumber && this.Value[this.SecondNumber - 1] == this.RequiredCharacter;
            return containsFirst ^ containsSecond;
        }

        public void Load(string input)
        {
            this.FirstNumber = int.Parse(input[..input.IndexOf('-')]);
            input = input[(input.IndexOf('-') + 1)..];
            this.SecondNumber = int.Parse(input[..input.IndexOf(' ')]);
            input = input[(input.IndexOf(' ') + 1)..];
            this.RequiredCharacter = input[0];
            input = input[(input.IndexOf(':') + 2)..];
            this.Value = input;
        }
    }
}
