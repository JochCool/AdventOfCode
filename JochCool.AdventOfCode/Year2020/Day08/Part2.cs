namespace JochCool.AdventOfCode.Year2020.Day08;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// parse the instructions in the file
		List<Instruction> instructions = [];
		foreach (string code in inputReader.ReadLines())
		{
			instructions.Add(Instruction.Parse(code));
		}

		// simply brute force. try every nop/jmp instruction and see if it works.
		int executionId = 0;
		int linenum = -1;
		foreach (Instruction instruction in instructions)
		{
			linenum++;
			OperationType oldType = instruction.Operation;
			switch (oldType)
			{
				case OperationType.Accumulator:
					continue; // this one must be correct according to assignment

				case OperationType.None:
					instruction.Operation = OperationType.Jump;
					break;

				case OperationType.Jump:
					instruction.Operation = OperationType.None;
					break;
			}

			if (TryExecute(instructions, executionId++, out int accumulator))
			{
				Console.WriteLine($"Found solution after {executionId} tries! Error is at line {linenum}. Accumulator = {accumulator}.");
				return accumulator.ToString();
			}

			// wrong answer; reset
			instruction.Operation = oldType;
		}

		return null;
	}

	// returns true if the program halts and doesn't jump out of the program.
	// execution id must be unique every time this list gets executed.
	static bool TryExecute(IList<Instruction> instructions, int executionId, out int accumulator)
	{
		int i = 0;
		accumulator = 0;
		while (true)
		{
			if (i == instructions.Count)
			{
				return true;
			}
			if (i > instructions.Count || instructions[i].LastExecutionId == executionId)
			{
				return false;
			}

			instructions[i].LastExecutionId = executionId; // This is so we know we've already been here.

			switch (instructions[i].Operation)
			{
				case OperationType.None:
					i++;
					break;

				case OperationType.Accumulator:
					accumulator += instructions[i].Argument;
					i++;
					break;

				case OperationType.Jump:
					i +=  instructions[i].Argument;
					break;
			}
		}
	}

	class Instruction
	{
		public OperationType Operation;
		public int Argument;

		// ID of the last time this instruction was executed.
		public int LastExecutionId = -1;

		public Instruction(OperationType operation, int argument)
		{
			Operation = operation;
			Argument = argument;
		}

		public static Instruction Parse(string code)
		{
			string[] arguments = code.Split(' ');
			if (arguments.Length != 2) throw new FormatException("Invalid amount of arguments.");
			OperationType type = (arguments[0]) switch
			{
				"nop" => OperationType.None,
				"acc" => OperationType.Accumulator,
				"jmp" => OperationType.Jump,
				_ => throw new FormatException($"Unknown operation type {arguments[0]}."),
			};
			return new Instruction(type, int.Parse(arguments[1]));
		}

		public override string ToString() => $"{Operation} {Argument}";
	}

	enum OperationType
	{
		None,
		Accumulator,
		Jump
	}
}
