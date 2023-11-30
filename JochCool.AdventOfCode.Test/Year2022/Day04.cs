using JochCool.AdventOfCode.Year2022.Day04;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day04
{
	[Theory]
	[PuzzleData("2", "day04_simple.txt")]
	[PuzzleData("538", "day04.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("4", "day04_simple.txt")]
	[PuzzleData("792", "day04.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
