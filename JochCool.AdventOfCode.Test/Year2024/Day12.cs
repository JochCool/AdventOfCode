using JochCool.AdventOfCode.Year2024.Day12;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day12
{
	[Theory]
	[PuzzleData("1930", "day12_simple.txt")]
	[PuzzleData("1483212", "day12.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1206", "day12_simple.txt")]
	[PuzzleData("897062", "day12.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
