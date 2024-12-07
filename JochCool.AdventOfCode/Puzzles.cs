using System.Collections.Immutable;

namespace JochCool.AdventOfCode;

public static class Puzzles
{
	public static readonly ImmutableArray<Puzzle> All = [
		new(2020, 1, 1, Year2020.Day01.Part1.Solve),
		new(2020, 1, 2, Year2020.Day01.Part2.Solve),
		new(2020, 2, 1, Year2020.Day02.Part1.Solve),
		new(2020, 2, 2, Year2020.Day02.Part2.Solve),
		new(2020, 3, 1, Year2020.Day03.Part1.Solve),
		new(2020, 3, 2, Year2020.Day03.Part2.Solve),
		new(2020, 4, 1, Year2020.Day04.Part1.Solve),
		new(2020, 4, 2, Year2020.Day04.Part2.Solve),
		new(2020, 5, 1, Year2020.Day05.Part1.Solve),
		new(2020, 5, 2, Year2020.Day05.Part2.Solve),
		new(2020, 6, 1, Year2020.Day06.Part1.Solve),
		new(2020, 6, 2, Year2020.Day06.Part2.Solve),
		new(2020, 7, 1, Year2020.Day07.Part1.Solve),
		new(2020, 7, 2, Year2020.Day07.Part2.Solve),
		new(2020, 8, 1, Year2020.Day08.Part1.Solve),
		new(2020, 8, 2, Year2020.Day08.Part2.Solve),
		new(2020, 9, 1, Year2020.Day09.Part1.Solve),
		new(2020, 9, 2, Year2020.Day09.Part2.Solve),
		new(2020, 10, 1, Year2020.Day10.Part1.Solve),
		new(2020, 10, 2, Year2020.Day10.Part2.Solve),
		new(2020, 11, 1, Year2020.Day11.Part1.Solve),
		new(2020, 11, 2, Year2020.Day11.Part2.Solve),
		new(2020, 12, 1, Year2020.Day12.Part1.Solve),
		new(2020, 12, 2, Year2020.Day12.Part2.Solve),
		new(2020, 13, 1, Year2020.Day13.Part1.Solve),
		new(2020, 13, 2, Year2020.Day13.Part2.Solve),
		new(2022, 1, 1, Year2022.Day01.Part1.Solve),
		new(2022, 1, 2, Year2022.Day01.Part2.Solve),
		new(2022, 2, 1, Year2022.Day02.Part1.Solve),
		new(2022, 2, 2, Year2022.Day02.Part2.Solve),
		new(2022, 3, 1, Year2022.Day03.Part1.Solve),
		new(2022, 3, 2, Year2022.Day03.Part2.Solve),
		new(2022, 4, 1, Year2022.Day04.Part1.Solve),
		new(2022, 4, 2, Year2022.Day04.Part2.Solve),
		new(2022, 5, 1, Year2022.Day05.Part1.Solve),
		new(2022, 5, 2, Year2022.Day05.Part2.Solve),
		new(2022, 6, 1, Year2022.Day06.Part1.Solve),
		new(2022, 6, 2, Year2022.Day06.Part2.Solve),
		new(2022, 7, 1, Year2022.Day07.Part1.Solve),
		new(2022, 7, 2, Year2022.Day07.Part2.Solve),
		new(2022, 8, 1, Year2022.Day08.Part1.Solve),
		new(2022, 8, 2, Year2022.Day08.Part2.Solve),
		new(2022, 9, 1, Year2022.Day09.Part1.Solve),
		new(2022, 9, 2, Year2022.Day09.Part2.Solve),
		new(2022, 10, 1, Year2022.Day10.Part1.Solve),
		new(2022, 10, 2, Year2022.Day10.Part2.Solve),
		new(2022, 11, 1, Year2022.Day11.Part1.Solve),
		new(2022, 11, 2, Year2022.Day11.Part2.Solve),
		new(2022, 12, 1, Year2022.Day12.Part1.Solve),
		new(2022, 12, 2, Year2022.Day12.Part2.Solve),
		new(2022, 13, 1, Year2022.Day13.Part1.Solve),
		new(2022, 13, 2, Year2022.Day13.Part2.Solve),
		new(2022, 14, 1, Year2022.Day14.Part1.Solve),
		new(2022, 14, 2, Year2022.Day14.Part2.Solve),
		new(2022, 15, 1, Year2022.Day15.Part1.Solve),
		new(2022, 15, 2, Year2022.Day15.Part2.Solve),
		new(2022, 16, 1, Year2022.Day16.Part1.Solve),
		new(2022, 16, 2, Year2022.Day16.Part2.Solve),
		new(2022, 17, 1, Year2022.Day17.Part1.Solve),
		new(2022, 17, 2, Year2022.Day17.Part2.Solve),
		new(2022, 18, 1, Year2022.Day18.Part1.Solve),
		new(2022, 18, 2, Year2022.Day18.Part2.Solve),
		new(2022, 20, 1, Year2022.Day20.Part1.Solve),
		new(2022, 20, 2, Year2022.Day20.Part2.Solve),
		new(2022, 21, 1, Year2022.Day21.Part1.Solve),
		new(2022, 21, 2, Year2022.Day21.Part2.Solve),
		new(2022, 22, 1, Year2022.Day22.Part1.Solve),
		new(2022, 22, 2, Year2022.Day22.Part2.Solve),
		new(2022, 23, 1, Year2022.Day23.Part1.Solve),
		new(2022, 23, 2, Year2022.Day23.Part2.Solve),
		new(2022, 24, 1, Year2022.Day24.Part1.Solve),
		new(2022, 24, 2, Year2022.Day24.Part2.Solve),
		new(2023, 1, 1, Year2023.Day01.Part1.Solve),
		new(2023, 1, 2, Year2023.Day01.Part2.Solve),
		new(2023, 2, 1, Year2023.Day02.Part1.Solve),
		new(2023, 2, 2, Year2023.Day02.Part2.Solve),
		new(2023, 3, 1, Year2023.Day03.Part1.Solve),
		new(2023, 3, 2, Year2023.Day03.Part2.Solve),
		new(2023, 4, 1, Year2023.Day04.Part1.Solve),
		new(2023, 4, 2, Year2023.Day04.Part2.Solve),
		new(2023, 5, 1, Year2023.Day05.Part1.Solve),
		new(2023, 5, 2, Year2023.Day05.Part2.Solve),
		new(2023, 6, 1, Year2023.Day06.Part1.Solve),
		new(2023, 6, 2, Year2023.Day06.Part2.Solve),
		new(2023, 7, 1, Year2023.Day07.Part1.Solve),
		new(2023, 7, 2, Year2023.Day07.Part2.Solve),
		new(2023, 8, 1, Year2023.Day08.Part1.Solve),
		new(2023, 8, 2, Year2023.Day08.Part2.Solve),
		new(2023, 9, 1, Year2023.Day09.Part1.Solve),
		new(2023, 9, 2, Year2023.Day09.Part2.Solve),
		new(2023, 10, 1, Year2023.Day10.Part1.Solve),
		new(2023, 10, 2, Year2023.Day10.Part2.Solve),
		new(2023, 11, 1, Year2023.Day11.Part1.Solve),
		new(2023, 11, 2, Year2023.Day11.Part2.Solve),
		new(2023, 12, 1, Year2023.Day12.Part1.Solve),
		new(2023, 12, 2, Year2023.Day12.Part2.Solve),
		new(2023, 13, 1, Year2023.Day13.Part1.Solve),
		new(2023, 13, 2, Year2023.Day13.Part2.Solve),
		new(2023, 14, 1, Year2023.Day14.Part1.Solve),
		new(2023, 14, 2, Year2023.Day14.Part2.Solve),
		new(2023, 15, 1, Year2023.Day15.Part1.Solve),
		new(2023, 15, 2, Year2023.Day15.Part2.Solve),
		new(2023, 16, 1, Year2023.Day16.Part1.Solve),
		new(2023, 16, 2, Year2023.Day16.Part2.Solve),
		new(2023, 17, 1, Year2023.Day17.Part1.Solve),
		new(2023, 17, 2, Year2023.Day17.Part2.Solve),
		new(2023, 18, 1, Year2023.Day18.Part1.Solve),
		new(2023, 18, 2, Year2023.Day18.Part2.Solve),
		new(2023, 19, 1, Year2023.Day19.Part1.Solve),
		new(2023, 19, 2, Year2023.Day19.Part2.Solve),
		new(2023, 20, 1, Year2023.Day20.Part1.Solve),
		new(2023, 20, 2, Year2023.Day20.Part2.Solve),
		new(2023, 21, 1, Year2023.Day21.Part1.Solve),
		new(2023, 21, 2, Year2023.Day21.Part2.Solve),
		new(2023, 22, 1, Year2023.Day22.Part1.Solve),
		new(2023, 22, 2, Year2023.Day22.Part2.Solve),
		new(2023, 23, 1, Year2023.Day23.Part1.Solve),
		new(2023, 23, 2, Year2023.Day23.Part2.Solve),
		new(2023, 25, 1, Year2023.Day25.Part1.Solve),
		new(2024, 1, 1, Year2024.Day01.Part1.Solve),
		new(2024, 1, 2, Year2024.Day01.Part2.Solve),
		new(2024, 2, 1, Year2024.Day02.Part1.Solve),
		new(2024, 2, 2, Year2024.Day02.Part2.Solve),
		new(2024, 3, 1, Year2024.Day03.Part1.Solve),
		new(2024, 3, 2, Year2024.Day03.Part2.Solve),
		new(2024, 4, 1, Year2024.Day04.Part1.Solve),
		new(2024, 4, 2, Year2024.Day04.Part2.Solve),
		new(2024, 5, 1, Year2024.Day05.Part1.Solve),
		new(2024, 5, 2, Year2024.Day05.Part2.Solve),
		new(2024, 6, 1, Year2024.Day06.Part1.Solve),
		new(2024, 6, 2, Year2024.Day06.Part2.Solve),
		new(2024, 7, 1, Year2024.Day07.Part1.Solve),
		new(2024, 7, 2, Year2024.Day07.Part2.Solve),
	];

	public static Puzzle? Get(int year, int day, int part)
	{
		foreach (Puzzle puzzle in All)
		{
			if (puzzle.Year == year && puzzle.Day == day && puzzle.Part == part)
			{
				return puzzle;
			}
		}
		return null;
	}
}
