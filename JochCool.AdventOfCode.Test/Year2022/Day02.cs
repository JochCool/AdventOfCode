using JochCool.AdventOfCode.Year2022.Day02;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day02
{
	[Theory]
	[PuzzleData("15", "day02_simple.txt")]
	[PuzzleData("12679", "day02.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("12", "day02_simple.txt")]
	[PuzzleData("14470", "day02.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
