using JochCool.AdventOfCode.Year2020.Day12;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day12
{
	[Theory]
	[PuzzleData("25", "day12_simple.txt")]
	[PuzzleData("1457", "day12.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("286", "day12_simple.txt")]
	[PuzzleData("106860", "day12.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
