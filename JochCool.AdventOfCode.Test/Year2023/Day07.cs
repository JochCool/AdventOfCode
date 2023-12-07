using JochCool.AdventOfCode.Year2023.Day07;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day07
{
	[Theory]
	[PuzzleData("6440", "day07_simple.txt")]
	[PuzzleData("248179786", "day07.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("5905", "day07_simple.txt")]
	[PuzzleData("247885995", "day07.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
