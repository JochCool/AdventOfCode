using JochCool.AdventOfCode.Year2022.Day17;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day17
{
	[Theory]
	[PuzzleData("3068", "day17_simple.txt")]
	[PuzzleData("3232", "day17.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1514285714288", "day17_simple.txt")]
	[PuzzleData("1585632183915", "day17.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
