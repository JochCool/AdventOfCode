using JochCool.AdventOfCode.Year2023.Day09;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day09
{
	[Theory]
	[PuzzleData("114", "day09_simple.txt")]
	[PuzzleData("1882395907", "day09.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("2", "day09_simple.txt")]
	[PuzzleData("1005", "day09.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
