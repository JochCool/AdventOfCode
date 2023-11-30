using JochCool.AdventOfCode.Year2020.Day06;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day06
{
	[Theory]
	[PuzzleData("6878", "day06.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("3464", "day06.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
