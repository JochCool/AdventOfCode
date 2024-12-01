namespace JochCool.AdventOfCode.Year2024.Day01;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		(List<int> left, List<int> right) = BothParts.ReadLists(inputReader);

		left.Sort();
		right.Sort();

		int sum = 0;
		for (int i = 0; i < left.Count; i++)
		{
			sum += int.Abs(left[i] - right[i]);
		}
		return sum.ToInvariantString();
	}
}
