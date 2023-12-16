using JochCool.AdventOfCode.Year2023.Day16;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day16
{
	[Theory]
	[PuzzleData("46", "day16_simple.txt")]
	[PuzzleData("7798", "day16.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("51", "day16_simple.txt")]
	[PuzzleData("8026", "day16.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
