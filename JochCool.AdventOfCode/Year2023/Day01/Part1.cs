namespace JochCool.AdventOfCode.Year2023.Day01;

public static class Part1
{
	static readonly char[] digits = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;
		foreach (string line in inputReader.ReadLines())
		{
			int firstDI = line.IndexOfAny(digits);
			int lastDI = line.LastIndexOfAny(digits);
			int num = int.Parse($"{line[firstDI]}{line[lastDI]}");
			sum += num;
		}

		return sum.ToInvariantString();
	}
}
