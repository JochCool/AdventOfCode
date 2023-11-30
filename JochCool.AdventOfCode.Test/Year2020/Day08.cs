using JochCool.AdventOfCode.Year2020.Day08;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day08
{
	[Theory]
	[PuzzleData("1179", "day08.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1089", "day08.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
