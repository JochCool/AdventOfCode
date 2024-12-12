using JochCool.AdventOfCode.Year2024.Day11;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day11
{
	[Theory]
	[PuzzleData("55312", "day11_simple.txt")]
	[PuzzleData("224529", "day11.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("266820198587914", "day11.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
