namespace JochCool.AdventOfCode.Year2024.Day01;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		(List<int> left, List<int> right) = BothParts.ReadLists(inputReader);

		int sum = 0;
		foreach (int num in left)
		{
			int multiplier = right.CountEqualTo(num);
			sum += num * multiplier;
		}
		return sum.ToInvariantString();
	}
}
