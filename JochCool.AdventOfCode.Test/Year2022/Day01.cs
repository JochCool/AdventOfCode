using JochCool.AdventOfCode.Year2022.Day01;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day01
{
	[Theory]
	[PuzzleData("24000", "day01_simple.txt")]
	[PuzzleData("66719", "day01.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("45000", "day01_simple.txt")]
	[PuzzleData("198551", "day01.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
