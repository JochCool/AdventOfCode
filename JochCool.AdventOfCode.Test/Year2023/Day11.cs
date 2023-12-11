using JochCool.AdventOfCode.Year2023.Day11;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day11
{
	[Theory]
	[PuzzleData("374", "day11_simple.txt")]
	[PuzzleData("9370588", "day11.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("746207878188", "day11.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
