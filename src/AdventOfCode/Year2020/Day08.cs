// <copyright file="Day08.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day08 : DayBase<Instruction>
    {
        public override int Number => 8;

        public override long Part1()
        {
            List<Instruction> instructions = this.GetInput().ToList();
            Simulator simulator = new();

            simulator.StepUntilDuplicateExecution(instructions);
            return simulator.AccumulatorValue;
        }

        public override long Part2()
        {
            List<Instruction> instructions = this.GetInput().ToList();
            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].Opcode != Instruction.Operation.Jump && instructions[i].Opcode != Instruction.Operation.NoOp)
                {
                    continue;
                }

                List<Instruction> instructionsModified = new(instructions)
                {
                    [i] = new Instruction
                    {
                        Opcode = instructions[i].Opcode == Instruction.Operation.Jump ? Instruction.Operation.NoOp : Instruction.Operation.Jump,
                        Argument = instructions[i].Argument,
                    },
                };

                Simulator simulator = new();
                simulator.StepUntilDuplicateExecution(instructionsModified);
                if (simulator.CurrentInstructionAddress == instructions.Count)
                {
                    Console.WriteLine($"Found swapped instruction at address {i}");
                    return simulator.AccumulatorValue;
                }
            }

            return -1;
        }
    }

    public class Simulator
    {
        public int AccumulatorValue { get; set; }

        public int CurrentInstructionAddress { get; set; }

        public void Step(IList<Instruction> instructions)
        {
            Instruction instruction = instructions[this.CurrentInstructionAddress];
            switch (instruction.Opcode)
            {
                case Instruction.Operation.Accumulate:
                    this.AccumulatorValue += instruction.Argument;
                    break;

                case Instruction.Operation.Jump:
                    this.CurrentInstructionAddress += instruction.Argument - 1; // -1 to account for unconditional +1 next
                    break;

                case Instruction.Operation.NoOp:
                    break;

                default:
                    throw new NotSupportedException($"Unsupported operation '{instruction.Opcode}'");
            }

            this.CurrentInstructionAddress += 1;
        }

        public void StepUntilDuplicateExecution(IList<Instruction> instructions)
        {
            HashSet<int> instructionsExecuted = new();
            for (int i = 0; true; i++)
            {
                if (instructionsExecuted.Contains(this.CurrentInstructionAddress))
                {
                    return;
                }
                else if (this.CurrentInstructionAddress < 0 || this.CurrentInstructionAddress >= instructions.Count)
                {
                    return;
                }
                instructionsExecuted.Add(this.CurrentInstructionAddress);

                this.Step(instructions);
            }
        }
    }

    public class Instruction : IDayInput
    {
        public enum Operation
        {
            Accumulate,
            Jump,
            NoOp,
        }

        public Operation Opcode { get; set; }

        public int Argument { get; set; }

        public bool Load(string input)
        {
            string[] split = input.Split(' ');

            this.Opcode = split[0] switch
            {
                "acc" => Operation.Accumulate,
                "jmp" => Operation.Jump,
                "nop" => Operation.NoOp,
                _ => throw new NotSupportedException($"Unknown operation '{split[0]}'"),
            };
            this.Argument = int.Parse(split[1]);

            return false;
        }
    }
}
