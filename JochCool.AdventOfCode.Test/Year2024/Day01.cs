using JochCool.AdventOfCode.Year2024.Day01;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day01
{
	[Theory]
	[PuzzleData("11", "day01_simple.txt")]
	[PuzzleData("1151792", "day01.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("31", "day01_simple.txt")]
	[PuzzleData("21790168", "day01.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
