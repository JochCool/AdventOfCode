namespace JochCool.AdventOfCode.Year2024.Day02;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		int result = inputReader.ReadLines().Count(report => BothParts.IsSafe(report, true));

		return result.ToInvariantString();
	}
}
