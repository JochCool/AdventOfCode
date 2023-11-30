using JochCool.AdventOfCode.Year2022.Day22;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day22
{
	[Theory]
	[PuzzleData("6032", "day22_simple.txt")]
	[PuzzleData("50412", "day22.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("5031", "day22_simple.txt")]
	[PuzzleData("130068", "day22.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
