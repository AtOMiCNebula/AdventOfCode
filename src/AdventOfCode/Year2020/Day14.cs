// <copyright file="Day14.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day14 : DayBase<MaskedMemory>
    {
        public override long Part1()
        {
            IEnumerable<MaskedMemory> instructions = this.GetInput();
            Dictionary<long, long> memory = new();
            foreach (MaskedMemory instruction in instructions)
            {
                memory[instruction.Address] = instruction.ValueForPart1;
            }

            return memory.Values.Sum();
        }

        public override long Part2()
        {
            IEnumerable<MaskedMemory> instructions = this.GetInput();
            Dictionary<long, long> memory = new();
            foreach (MaskedMemory instruction in instructions)
            {
                foreach (long address in instruction.AddressesForPart2)
                {
                    memory[address] = instruction.Value;
                }
            }

            return memory.Values.Sum();
        }
    }

    public class MaskedMemory : IDayInput
    {
        public static long CurrentZeroesMask { get; set; }

        public static long CurrentOnesMask { get; set; }

        public static long CurrentFloatingMask { get; set; }

        public long Address { get; set; }

        public long Value { get; set; }

        public long ZeroesMask { get; set; }

        public long OnesMask { get; set; }

        public long FloatingMask { get; set; }

        public long ValueForPart1
        {
            get
            {
                return (this.Value & this.ZeroesMask) | this.OnesMask;
            }
        }

        public IEnumerable<long> AddressesForPart2
        {
            get
            {
                List<(long Zeroes, long Ones)> permutations = new();
                for (int i = 35; i >= 0; i--)
                {
                    if ((this.FloatingMask & (1L << i)) != 0)
                    {
                        List<(long Zeroes, long Ones)> newPermutations = new();
                        if (permutations.Count > 0)
                        {
                            foreach ((long zeroesMask, long onesMask) in permutations)
                            {
                                newPermutations.Add((zeroesMask | (1L << i), onesMask));
                                newPermutations.Add((zeroesMask, onesMask | (1L << i)));
                            }
                        }
                        else
                        {
                            newPermutations.Add((1L << i, 0));
                            newPermutations.Add((0, 1L << i));
                        }

                        permutations = newPermutations;
                    }
                }

                foreach ((long permutedZeroesMask, long permutedOnesMask) in permutations)
                {
                    yield return (this.Address & ~permutedZeroesMask & ~this.ZeroesMask) | permutedOnesMask | this.OnesMask;
                }
            }
        }

        public bool Load(string input)
        {
            if (input.StartsWith("mask = "))
            {
                MaskedMemory.CurrentZeroesMask = 0;
                MaskedMemory.CurrentOnesMask = 0;
                MaskedMemory.CurrentFloatingMask = 0;

                string mask = input["mask = ".Length..];
                foreach (char maskBit in mask)
                {
                    MaskedMemory.CurrentZeroesMask <<= 1;
                    if (maskBit != '0')
                    {
                        MaskedMemory.CurrentZeroesMask |= 1;
                    }

                    MaskedMemory.CurrentOnesMask <<= 1;
                    if (maskBit == '1')
                    {
                        MaskedMemory.CurrentOnesMask |= 1;
                    }

                    MaskedMemory.CurrentFloatingMask <<= 1;
                    if (maskBit == 'X')
                    {
                        MaskedMemory.CurrentFloatingMask |= 1;
                    }
                }

                return true;
            }
            else
            {
                input = input["mem[".Length..];
                this.Address = long.Parse(input[..input.IndexOf(']')]);
                this.Value = long.Parse(input[(input.IndexOf(" = ") + 3)..]);
                this.ZeroesMask = MaskedMemory.CurrentZeroesMask;
                this.OnesMask = MaskedMemory.CurrentOnesMask;
                this.FloatingMask = MaskedMemory.CurrentFloatingMask;
                return false;
            }
        }
    }
}
