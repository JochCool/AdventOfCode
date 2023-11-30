using JochCool.AdventOfCode.Year2022.Day09;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day09
{
	[Theory]
	[PuzzleData("13", "day09_simple.txt")]
	[PuzzleData("6081", "day09.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1", "day09_simple.txt")]
	[PuzzleData("36", "day09_simple2.txt")]
	[PuzzleData("2487", "day09.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
