using JochCool.AdventOfCode.Year2023.Day02;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day02
{
	[Theory]
	[PuzzleData("8", "day02_simple.txt")]
	[PuzzleData("2268", "day02.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("2286", "day02_simple.txt")]
	[PuzzleData("63542", "day02.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
