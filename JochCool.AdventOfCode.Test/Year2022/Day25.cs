using JochCool.AdventOfCode.Year2022.Day25;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day25
{
	[Theory]
	[PuzzleData("2=-1=0", "day25_simple.txt")]
	[PuzzleData("20-==01-2-=1-2---1-0", "day25.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}
}
