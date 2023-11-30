using JochCool.AdventOfCode.Year2022.Day15;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day15
{
	[Theory]
	[PuzzleData("26", "day15_simple.txt")]
	[PuzzleData("5083287", "day15.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("56000011", "day15_simple.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
