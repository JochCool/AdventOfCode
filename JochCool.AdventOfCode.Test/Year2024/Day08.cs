using JochCool.AdventOfCode.Year2024.Day08;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day08
{
	[Theory]
	[PuzzleData("14", "day08_simple.txt")]
	[PuzzleData("271", "day08.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("34", "day08_simple.txt")]
	[PuzzleData("994", "day08.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
