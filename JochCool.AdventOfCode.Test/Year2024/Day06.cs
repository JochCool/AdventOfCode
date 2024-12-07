using JochCool.AdventOfCode.Year2024.Day06;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day06
{
	[Theory]
	[PuzzleData("41", "day06_simple.txt")]
	[PuzzleData("5409", "day06.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("6", "day06_simple.txt")]
	[PuzzleData("2022", "day06.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
