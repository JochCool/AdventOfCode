using JochCool.AdventOfCode.Year2023.Day04;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day04
{
	[Theory]
	[PuzzleData("13", "day04_simple.txt")]
	[PuzzleData("20829", "day04.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("30", "day04_simple.txt")]
	[PuzzleData("12648035", "day04.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
