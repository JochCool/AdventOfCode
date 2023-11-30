using JochCool.AdventOfCode.Year2020.Day03;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day03
{
	[Theory]
	[PuzzleData("7", "day03_simple.txt")]
	[PuzzleData("286", "day03.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("336", "day03_simple.txt")]
	[PuzzleData("3638606400", "day03.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
