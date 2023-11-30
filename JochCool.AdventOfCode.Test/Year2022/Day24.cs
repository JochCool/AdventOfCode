using JochCool.AdventOfCode.Year2022.Day24;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day24
{
	[Theory]
	[PuzzleData("18", "day24_simple.txt")]
	[PuzzleData("264", "day24.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("54", "day24_simple.txt")]
	[PuzzleData("789", "day24.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
