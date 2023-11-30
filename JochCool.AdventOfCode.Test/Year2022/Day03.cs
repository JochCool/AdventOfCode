using JochCool.AdventOfCode.Year2022.Day03;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day03
{
	[Theory]
	[PuzzleData("157", "day03_simple.txt")]
	[PuzzleData("7850", "day03.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("70", "day03_simple.txt")]
	[PuzzleData("2581", "day03.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
