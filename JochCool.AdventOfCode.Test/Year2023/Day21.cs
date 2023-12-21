using JochCool.AdventOfCode.Year2023.Day21;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day21
{
	[Theory]
	[PuzzleData("3682", "day21.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}
}
