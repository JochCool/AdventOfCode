using JochCool.AdventOfCode.Year2022.Day12;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day12
{
	[Theory]
	[PuzzleData("31", "day12_simple.txt")]
	[PuzzleData("472", "day12.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("29", "day12_simple.txt")]
	[PuzzleData("465", "day12.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
