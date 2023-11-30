using JochCool.AdventOfCode.Year2020.Day11;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day11
{
	[Theory]
	[PuzzleData("37", "day11_simple.txt")]
	[PuzzleData("2472", "day11.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("26", "day11_simple.txt")]
	[PuzzleData("2197", "day11.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
