using JochCool.AdventOfCode.Year2024.Day02;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day02
{
	[Theory]
	[PuzzleData("2", "day02_simple.txt")]
	[PuzzleData("591", "day02.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("4", "day02_simple.txt")]
	[PuzzleData("621", "day02.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
