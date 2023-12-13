using JochCool.AdventOfCode.Year2023.Day13;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day13
{
	[Theory]
	[PuzzleData("405", "day13_simple.txt")]
	[PuzzleData("36041", "day13.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("400", "day13_simple.txt")]
	[PuzzleData("35915", "day13.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
