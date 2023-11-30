using JochCool.AdventOfCode.Year2022.Day11;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day11
{
	[Theory]
	[PuzzleData("10605", "day11_simple.txt")]
	[PuzzleData("72884", "day11.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("2713310158", "day11_simple.txt")]
	[PuzzleData("15310845153", "day11.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
