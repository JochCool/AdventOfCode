using JochCool.AdventOfCode.Year2022.Day16;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day16
{
	[Theory]
	[PuzzleData("1651", "day16_simple.txt")]
	[PuzzleData("2320", "day16.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("1707", "day16_simple.txt")]
#if ALL_TESTS
	[PuzzleData("2967", "day16.txt")] // This test takes over 2 minutes to run
#endif
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}
}
