using JochCool.AdventOfCode.Year2022.Day14;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day14
{
	[Theory]
	[PuzzleData("24", "day14_simple.txt")]
	[PuzzleData("698", "day14.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("93", "day14_simple.txt")]
	[PuzzleData("28594", "day14.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
