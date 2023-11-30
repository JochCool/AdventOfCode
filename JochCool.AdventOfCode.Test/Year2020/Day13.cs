using JochCool.AdventOfCode.Year2020.Day13;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day13
{
	[Theory]
	[PuzzleData("295", "day13_simple.txt")]
	[PuzzleData("8063", "day13.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1068781", "day13_simple.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
