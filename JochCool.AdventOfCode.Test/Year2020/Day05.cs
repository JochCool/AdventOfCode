using JochCool.AdventOfCode.Year2020.Day05;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day05
{
	[Theory]
	[PuzzleData("888", "day05.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("522", "day05.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
