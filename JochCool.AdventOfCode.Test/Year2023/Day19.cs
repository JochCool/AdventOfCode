using JochCool.AdventOfCode.Year2023.Day19;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day19
{
	[Theory]
	[PuzzleData("19114", "day19_simple.txt")]
	[PuzzleData("509597", "day19.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("167409079868000", "day19_simple.txt")]
	[PuzzleData("143219569011526", "day19.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
