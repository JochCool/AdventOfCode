using JochCool.AdventOfCode.Year2024.Day03;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day03
{
	[Theory]
	[PuzzleData("161", "day03_simple.txt")]
	[PuzzleData("175700056", "day03.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("48", "day03_simple2.txt")]
	[PuzzleData("71668682", "day03.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
