using JochCool.AdventOfCode.Year2022.Day23;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day23
{
	[Theory]
	[PuzzleData("25", "day23_simple.txt")]
	[PuzzleData("110", "day23_simple2.txt")]
	[PuzzleData("3862", "day23.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("4", "day23_simple.txt")]
	[PuzzleData("20", "day23_simple2.txt")]
	[PuzzleData("913", "day23.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
