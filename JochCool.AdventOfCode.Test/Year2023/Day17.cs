using JochCool.AdventOfCode.Year2023.Day17;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day17
{
	[Theory]
	[PuzzleData("102", "day17_simple.txt")]
	[PuzzleData("1023", "day17.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("94", "day17_simple.txt")]
	[PuzzleData("71", "day17_simple2.txt")]
	[PuzzleData("1165", "day17.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
