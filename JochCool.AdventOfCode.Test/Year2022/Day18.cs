using JochCool.AdventOfCode.Year2022.Day18;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day18
{
	[Theory]
	[PuzzleData("64", "day18_simple.txt")]
	[PuzzleData("4504", "day18.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("58", "day18_simple.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
