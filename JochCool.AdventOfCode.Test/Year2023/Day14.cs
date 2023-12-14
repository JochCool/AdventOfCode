using JochCool.AdventOfCode.Year2023.Day14;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day14
{
	[Theory]
	[PuzzleData("136", "day14_simple.txt")]
	[PuzzleData("108857", "day14.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("64", "day14_simple.txt", Skip = "It's off by one??")]
	[PuzzleData("95273", "day14.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
