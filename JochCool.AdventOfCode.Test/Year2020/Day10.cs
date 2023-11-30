using JochCool.AdventOfCode.Year2020.Day10;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day10
{
	[Theory]
	[PuzzleData("35", "day10_simple.txt")]
	[PuzzleData("2376", "day10.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("8", "day10_simple.txt")]
	[PuzzleData("129586085429248", "day10.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
