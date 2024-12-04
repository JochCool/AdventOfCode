using JochCool.AdventOfCode.Year2024.Day04;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day04
{
	[Theory]
	[PuzzleData("18", "day04_simple.txt")]
	[PuzzleData("2524", "day04.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("9", "day04_simple.txt")]
	[PuzzleData("1873", "day04.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
