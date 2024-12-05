using JochCool.AdventOfCode.Year2024.Day05;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day05
{
	[Theory]
	[PuzzleData("143", "day05_simple.txt")]
	[PuzzleData("6951", "day05.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("123", "day05_simple.txt")]
	[PuzzleData("4121", "day05.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
