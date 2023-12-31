namespace JochCool.AdventOfCode.Year2022;

public static class Solutions
{
	public static (Solution Part1, Solution Part2) Get(int day)
	{
		return day switch
		{
			1 => (Day01.Part1.Solve, Day01.Part2.Solve),
			2 => (Day02.Part1.Solve, Day02.Part2.Solve),
			3 => (Day03.Part1.Solve, Day03.Part2.Solve),
			4 => (Day04.Part1.Solve, Day04.Part2.Solve),
			5 => (Day05.Part1.Solve, Day05.Part2.Solve),
			6 => (Day06.Part1.Solve, Day06.Part2.Solve),
			7 => (Day07.Part1.Solve, Day07.Part2.Solve),
			8 => (Day08.Part1.Solve, Day08.Part2.Solve),
			9 => (Day09.Part1.Solve, Day09.Part2.Solve),
			10 => (Day10.Part1.Solve, Day10.Part2.Solve),
			11 => (Day11.Part1.Solve, Day11.Part2.Solve),
			12 => (Day12.Part1.Solve, Day12.Part2.Solve),
			13 => (Day13.Part1.Solve, Day13.Part2.Solve),
			14 => (Day14.Part1.Solve, Day14.Part2.Solve),
			15 => (Day15.Part1.Solve, Day15.Part2.Solve),
			16 => (Day16.Part1.Solve, Day16.Part2.Solve),
			17 => (Day17.Part1.Solve, Day17.Part2.Solve),
			18 => (Day18.Part1.Solve, Day18.Part2.Solve),
			20 => (Day20.Part1.Solve, Day20.Part2.Solve),
			21 => (Day21.Part1.Solve, Day21.Part2.Solve),
			22 => (Day22.Part1.Solve, Day22.Part2.Solve),
			23 => (Day23.Part1.Solve, Day23.Part2.Solve),
			24 => (Day24.Part1.Solve, Day24.Part2.Solve),
			19 or 25 => throw new NotImplementedException("This puzzle is yet to be solved."),
			_ => throw new ArgumentException($"{day} is not a puzzle day!", nameof(day))
		};
	}
}
