using JochCool.AdventOfCode.Year2022.Day06;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day06
{
	[Theory]
	[PuzzleData("7", "day06_simple.txt")]
	[PuzzleData("5", "day06_simple2.txt")]
	[PuzzleData("6", "day06_simple3.txt")]
	[PuzzleData("10", "day06_simple4.txt")]
	[PuzzleData("11", "day06_simple5.txt")]
	[PuzzleData("1760", "day06.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("19", "day06_simple.txt")]
	[PuzzleData("23", "day06_simple2.txt")]
	[PuzzleData("23", "day06_simple3.txt")]
	[PuzzleData("29", "day06_simple4.txt")]
	[PuzzleData("26", "day06_simple5.txt")]
	[PuzzleData("2974", "day06.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
