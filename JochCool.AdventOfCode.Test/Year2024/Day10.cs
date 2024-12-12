using JochCool.AdventOfCode.Year2024.Day10;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day10
{
	[Theory]
	[PuzzleData("36", "day10_simple.txt")]
	[PuzzleData("593", "day10.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("81", "day10_simple.txt")]
	[PuzzleData("1192", "day10.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
