using JochCool.AdventOfCode.Year2023.Day22;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day22
{
	[Theory]
	[PuzzleData("5", "day22_simple.txt")]
	[PuzzleData("475", "day22.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("7", "day22_simple.txt")]
	[PuzzleData("79144", "day22.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
