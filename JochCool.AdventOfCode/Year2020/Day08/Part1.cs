namespace JochCool.AdventOfCode.Year2020.Day08;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string?[] instructions = inputReader.ReadAllLines();
		int i = 0;
		int accumulator = 0;
		while (true)
		{
			if (i == instructions.Length)
			{
				Console.WriteLine("Reached the end of the program.");
				break;
			}
			if (i > instructions.Length)
			{
				Console.WriteLine($"Reached past the end of the program at {i}.");
				break;
			}
			string? instruction = instructions[i];
			if (instruction is null)
			{
				Console.WriteLine($"Encountered infinite loop at instruction {i}.");
				break;
			}

			string[] arguments = instruction.Split(' ');
			instructions[i] = null; // This is so we know we've already been here.

			switch (arguments[0]) {
				case "nop":
					i++;
					break;

				case "acc":
					accumulator += int.Parse(arguments[1]);
					i++;
					break;

				case "jmp":
					i += int.Parse(arguments[1]);
					break;
			}
		}

		Console.WriteLine($"Accumulator = {accumulator}.");

		return accumulator.ToString();
	}
}
