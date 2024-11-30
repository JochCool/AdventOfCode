namespace JochCool.AdventOfCode.Year2022.Day01;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int highest = int.MinValue;
		int current = 0;

		foreach (string line in inputReader.ReadLines())
		{
			if (line.Length == 0)
			{
				if (current > highest) highest = current;
				current = 0;
				continue;
			}
			current += int.Parse(line, CultureInfo.InvariantCulture);
		}
		if (current > highest) highest = current;

		return highest.ToInvariantString();
	}
}
