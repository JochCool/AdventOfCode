using JochCool.AdventOfCode.Year2023.Day23;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day23
{
	[Theory]
	[PuzzleData("94", "day23_simple.txt")]
	[PuzzleData("1966", "day23.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("154", "day23_simple.txt")]
	[PuzzleData("6286", "day23.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
