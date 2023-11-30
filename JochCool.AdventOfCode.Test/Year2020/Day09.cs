using JochCool.AdventOfCode.Year2020.Day09;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day09
{
	[Theory]
	[PuzzleData("675280050", "day09.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("96081673", "day09.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
