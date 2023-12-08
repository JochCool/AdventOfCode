using JochCool.AdventOfCode.Year2023.Day08;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day08
{
	[Theory]
	[PuzzleData("2", "day08_simple.txt")]
	[PuzzleData("6", "day08_simple2.txt")]
	[PuzzleData("17621", "day08.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("2", "day08_simple.txt")]
	[PuzzleData("6", "day08_simple2.txt")]
	[PuzzleData("6", "day08_simple3.txt")]
	[PuzzleData("20685524831999", "day08.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
