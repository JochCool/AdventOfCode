using JochCool.AdventOfCode.Year2023.Day01;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day01
{
	[Theory]
	[PuzzleData("142", "day01_simple.txt")]
	[PuzzleData("54927", "day01.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("281", "day01_simple2.txt")]
	[PuzzleData("54581", "day01.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
