using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulousIndustries.AdventOfCode.Year2020
{
    public class Day4 : DayBase<PassportLine>
    {
        public override int Number => 4;

        public override void Part1()
        {
            IEnumerable<Passport> passports = this.ProcessPassports(this.GetInput().Select(p => p.Value));
            Console.WriteLine($"Counted {passports.Where(p => p.IsValidForPart1()).Count()} valid passports (part 1)");
        }

        public override void Part2()
        {
            IEnumerable<Passport> passports = this.ProcessPassports(this.GetInput().Select(p => p.Value));
            Console.WriteLine($"Counted {passports.Where(p => p.IsValidForPart2()).Count()} valid passports (part 2)");
        }

        protected IEnumerable<Passport> ProcessPassports(IEnumerable<string> passportsRaw)
        {
            Passport passportCurrent = new Passport();
            List<Passport> passports = new List<Passport>() { passportCurrent };
            foreach (string passportLine in passportsRaw)
            {
                int tuplesRead = passportCurrent.ProcessLine(passportLine);
                if (tuplesRead == 0)
                {
                    passportCurrent = new Passport();
                    passports.Add(passportCurrent);
                }
            }

            return passports;
        }
    }

    public class PassportLine : IDayInput
    {
        public string Value { get; set; }

        public void Load(string input)
        {
            this.Value = input;
        }
    }

    public class Passport
    {
        public string BirthYear { get; set; }

        public string IssueYear { get; set; }

        public string ExpirationYear { get; set; }

        public string Height { get; set; }

        public string HairColor { get; set; }

        public string EyeColor { get; set; }

        public string PassportID { get; set; }

        public string CountryID { get; set; }

        public bool IsValidForPart1()
        {
            return
                this.BirthYear != null &&
                this.IssueYear != null &&
                this.ExpirationYear != null &&
                this.Height != null &&
                this.HairColor != null &&
                this.EyeColor != null &&
                this.PassportID != null;
        }

        public bool IsValidForPart2()
        {
            if (!this.IsValidForPart1())
            {
                return false;
            }

            int birthYear = int.Parse(this.BirthYear);
            if (!(1920 <= birthYear && birthYear <= 2002))
            {
                return false;
            }

            int issueYear = int.Parse(this.IssueYear);
            if (!(2010 <= issueYear && issueYear <= 2020))
            {
                return false;
            }

            int expirationYear = int.Parse(this.ExpirationYear);
            if (!(2020 <= expirationYear && expirationYear <= 2030))
            {
                return false;
            }

            if (!this.Height.EndsWith("cm") && !this.Height.EndsWith("in"))
            {
                return false;
            }
            int height = int.Parse(this.Height.Substring(0, this.Height.Length - 2));
            if ((this.Height.EndsWith("cm") && !(150 <= height && height <= 193))
                || (this.Height.EndsWith("in") && !(59 <= height && height <= 76)))
            {
                return false;
            }

            if (this.HairColor.Length != 7 || this.HairColor[0] != '#')
            {
                return false;
            }
            for (int i = 1; i < 7; i++)
            {
                if (!('0' <= this.HairColor[i] && this.HairColor[i] <= '9') && !('a' <= this.HairColor[i] && this.HairColor[i] <= 'f'))
                {
                    return false;
                }
            }

            switch (this.EyeColor)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    break;

                default:
                    return false;
            }

            if (this.PassportID.Length != 9 || !int.TryParse(this.PassportID, out _))
            {
                return false;
            }

            return true;
        }

        public int ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return 0;
            }

            string[] splits = line.Split(' ');
            foreach (string split in splits)
            {
                string[] kv = split.Split(':');
                switch (kv[0])
                {
                    case "byr":
                        this.BirthYear = kv[1];
                        break;
                    case "iyr":
                        this.IssueYear = kv[1];
                        break;
                    case "eyr":
                        this.ExpirationYear = kv[1];
                        break;
                    case "hgt":
                        this.Height = kv[1];
                        break;
                    case "hcl":
                        this.HairColor = kv[1];
                        break;
                    case "ecl":
                        this.EyeColor = kv[1];
                        break;
                    case "pid":
                        this.PassportID = kv[1];
                        break;
                    case "cid":
                        this.CountryID = kv[1];
                        break;
                    default:
                        throw new NotSupportedException($"Unexpected passport key '{kv[0]}' with value '{kv[1]}'");
                }
            }

            return splits.Length;
        }
    }
}
