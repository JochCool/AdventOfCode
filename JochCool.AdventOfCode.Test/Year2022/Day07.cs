using JochCool.AdventOfCode.Year2022.Day07;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day07
{
	[Theory]
	[PuzzleData("95437", "day07_simple.txt")]
	[PuzzleData("1501149", "day07.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("24933642", "day07_simple.txt")]
	[PuzzleData("10096985", "day07.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
