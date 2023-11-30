using JochCool.AdventOfCode.Year2022.Day08;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day08
{
	[Theory]
	[PuzzleData("21", "day08_simple.txt")]
	[PuzzleData("1776", "day08.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("8", "day08_simple.txt")]
	[PuzzleData("234416", "day08.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
