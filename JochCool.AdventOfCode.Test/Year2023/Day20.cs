using JochCool.AdventOfCode.Year2023.Day20;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day20
{
	[Theory]
	[PuzzleData("32000000", "day20_simple.txt")]
	[PuzzleData("11687500", "day20_simple2.txt")]
	[PuzzleData("929810733", "day20.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("231657829136023", "day20.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
