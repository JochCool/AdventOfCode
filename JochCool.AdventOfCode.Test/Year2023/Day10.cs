using JochCool.AdventOfCode.Year2023.Day10;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day10
{
	[Theory]
	[PuzzleData("4", "day10_simple.txt")]
	[PuzzleData("4", "day10_simple2.txt")]
	[PuzzleData("8", "day10_simple3.txt")]
	[PuzzleData("8", "day10_simple4.txt")]
	[PuzzleData("6640", "day10.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1", "day10_simple.txt")]
	[PuzzleData("1", "day10_simple2.txt")]
	[PuzzleData("1", "day10_simple3.txt")]
	[PuzzleData("1", "day10_simple4.txt")]
	[PuzzleData("4", "day10_simple5.txt")]
	[PuzzleData("4", "day10_simple6.txt")]
	[PuzzleData("8", "day10_simple7.txt")]
	[PuzzleData("10", "day10_simple8.txt")]
	[PuzzleData("411", "day10.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
