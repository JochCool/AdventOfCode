using JochCool.AdventOfCode.Year2023.Day18;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day18
{
	[Theory]
	[PuzzleData("62", "day18_simple.txt")]
	[PuzzleData("46359", "day18.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("952408144115", "day18_simple.txt")]
	[PuzzleData("59574883048274", "day18.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
