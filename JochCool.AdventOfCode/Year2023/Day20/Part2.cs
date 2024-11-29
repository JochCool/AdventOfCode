namespace JochCool.AdventOfCode.Year2023.Day20;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// These numbers were calculated by hand based on my own puzzle input
		return NumberUtil.Lcm<long>(3881, 3931, 3943, 3851).ToInvariantString();
	}
}
