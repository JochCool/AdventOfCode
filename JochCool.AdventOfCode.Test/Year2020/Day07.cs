using JochCool.AdventOfCode.Year2020.Day07;

namespace JochCool.AdventOfCode.Test.Year2020;

public static class Day07
{
	[Theory]
	[PuzzleData("164", "day07.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("7872", "day07.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
