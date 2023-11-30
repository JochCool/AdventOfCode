using JochCool.AdventOfCode.Year2022.Day13;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day13
{
	[Theory]
	[PuzzleData("13", "day13_simple.txt")]
	[PuzzleData("6187", "day13.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("140", "day13_simple.txt")]
	[PuzzleData("23520", "day13.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
