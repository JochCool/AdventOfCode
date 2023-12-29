using JochCool.AdventOfCode.Year2023.Day25;

namespace JochCool.AdventOfCode.Test.Year2023;

public static class Day25
{
	[Theory]
	[PuzzleData("54", "day25_simple.txt")]
	[PuzzleData("571753", "day25.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}
}
