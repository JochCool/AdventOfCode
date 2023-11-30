using JochCool.AdventOfCode.Year2022.Day21;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day21
{
	[Theory]
	[PuzzleData("152", "day21_simple.txt")]
	[PuzzleData("72664227897438", "day21.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("301", "day21_simple.txt")]
	[PuzzleData("3916491093817", "day21.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
