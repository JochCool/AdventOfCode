using JochCool.AdventOfCode.Year2023.Day12;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day12
{
	[Theory]
	[PuzzleData("21", "day12_simple.txt")]
	[PuzzleData("7939", "day12.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}
}
