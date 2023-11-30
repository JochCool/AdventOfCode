using JochCool.AdventOfCode.Year2022.Day20;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day20
{
	[Theory]
	[PuzzleData("3", "day20_simple.txt")]
	[PuzzleData("2622", "day20.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1623178306", "day20_simple.txt")]
	[PuzzleData("1538773034088", "day20.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
