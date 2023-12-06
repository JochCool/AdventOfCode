using JochCool.AdventOfCode.Year2023.Day06;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day06
{
	[Theory]
	[PuzzleData("288", "day06_simple.txt")]
	[PuzzleData("861300", "day06.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("71503", "day06_simple.txt")]
	[PuzzleData("28101347", "day06.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
