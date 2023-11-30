using JochCool.AdventOfCode.Year2020.Day02;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day02
{
	[Theory]
	[PuzzleData("2", "day02_simple.txt")]
	[PuzzleData("447", "day02.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1", "day02_simple.txt")]
	[PuzzleData("249", "day02.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
