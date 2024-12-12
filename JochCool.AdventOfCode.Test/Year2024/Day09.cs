using JochCool.AdventOfCode.Year2024.Day09;

namespace JochCool.AdventOfCode.Test.Year2024;

public static class Day09
{
	[Theory]
	[PuzzleData("1928", "day09_simple.txt")]
	[PuzzleData("6337367222422", "day09.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData("2858", "day09_simple.txt")]
	[PuzzleData("6361380647183", "day09.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}

	[Fact]
	public static void TestFilesystemChecksumCalculator()
	{
		FilesystemChecksumCalculator calculator = new();
		calculator.AddFile(0, 2);
		calculator.AddFile(1, 3);
		calculator.AddFreeSpace(1);
		calculator.AddFile(2, 4);
		Assert.Equal(69, calculator.Result);
	}
}
