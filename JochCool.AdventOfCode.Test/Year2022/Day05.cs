using JochCool.AdventOfCode.Year2022.Day05;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day05
{
	[Theory]
	[PuzzleData("CMZ", "day05_simple.txt")]
	[PuzzleData("LBLVVTVLP", "day05.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("MCD", "day05_simple.txt")]
	[PuzzleData("TPFFBDRJD", "day05.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
