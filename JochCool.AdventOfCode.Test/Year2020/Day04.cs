using JochCool.AdventOfCode.Year2020.Day04;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day04
{
	[Theory]
	[PuzzleData("2", "day04_simple.txt")]
	[PuzzleData("204", "day04.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("179", "day04.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
