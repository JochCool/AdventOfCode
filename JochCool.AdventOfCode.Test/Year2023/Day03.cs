using JochCool.AdventOfCode.Year2023.Day03;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day03
{
	[Theory]
	[PuzzleData("4361", "day03_simple.txt")]
	[PuzzleData("525911", "day03.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("467835", "day03_simple.txt")]
	[PuzzleData("75805607", "day03.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
