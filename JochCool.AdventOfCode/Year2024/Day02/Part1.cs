namespace JochCool.AdventOfCode.Year2024.Day02;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int result = inputReader.ReadLines().Count(report => BothParts.IsSafe(report, false));

		return result.ToInvariantString();
	}
}
