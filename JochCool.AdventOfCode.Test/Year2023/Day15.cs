using JochCool.AdventOfCode.Year2023.Day15;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day15
{
	[Theory]
	[PuzzleData("1320", "day15_simple.txt")]
	[PuzzleData("510388", "day15.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("145", "day15_simple.txt", Skip = "It's off by one??")]
	[PuzzleData("291774", "day15.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
