namespace JochCool.AdventOfCode.Year2022.Day10;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		BigInteger total = 0;
		BigInteger x = 1;
		int cycle = 0;
		int targetCycle = 20;

		foreach (string line in inputReader.ReadLines())
		{
			if (line == "noop")
			{
				cycle++;
			}
			else if (!line.StartsWith("addx "))
			{
				Console.WriteLine("Unknown instruction " + line);
				return null;
			}
			else
			{
				cycle += 2;
				x += BigInteger.Parse(line.AsSpan(5));
			}
			if (cycle == targetCycle - 1 || cycle == targetCycle - 2)
			{
				Console.WriteLine($"At {cycle}, x is {x}");
				total += targetCycle * x;
				targetCycle += 40;
			}
		}
		return total.ToString();
	}
}
