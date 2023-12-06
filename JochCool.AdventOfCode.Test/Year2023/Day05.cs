using JochCool.AdventOfCode.Year2023.Day05;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day05
{
	[Theory]
	[PuzzleData("35", "day05_simple.txt")]
	[PuzzleData("331445006", "day05.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("46", "day05_simple.txt")]
	[PuzzleData("6472060", "day05.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
