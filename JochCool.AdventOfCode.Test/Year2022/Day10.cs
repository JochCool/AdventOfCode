using JochCool.AdventOfCode.Year2022.Day10;

namespace JochCool.AdventOfCode.Test.Year2022;

public static class Day10
{
	[Theory]
	[PuzzleData("13140", "day10_simple.txt")]
	[PuzzleData("11220", "day10.txt")]
	public static void TestPart1(string expected, TextReader input)
	{
		Assert.Equal(expected, Part1.Solve(input));
	}

	[Theory]
	[PuzzleData(part2SimpleResult, "day10_simple.txt")]
	[PuzzleData(part2Result, "day10.txt")]
	public static void TestPart2(string expected, TextReader input)
	{
		Assert.Equal(expected, Part2.Solve(input));
	}

	const string part2SimpleResult = """
		##..##..##..##..##..##..##..##..##..##..
		###...###...###...###...###...###...###.
		####....####....####....####....####....
		#####.....#####.....#####.....#####.....
		######......######......######......####
		#######.......#######.......#######.....

		""";

	const string part2Result = """
		###..####.###...##....##.####.#....#..#.
		#..#....#.#..#.#..#....#.#....#....#.#..
		###....#..#..#.#..#....#.###..#....##...
		#..#..#...###..####....#.#....#....#.#..
		#..#.#....#....#..#.#..#.#....#....#.#..
		###..####.#....#..#..##..####.####.#..#.
		
		""";
}
