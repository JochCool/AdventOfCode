namespace JochCool.AdventOfCode.Year2020;

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
			> 13 and <= 25 => throw new NotImplementedException("These puzzles are yet to be solved.")
			_ => throw new ArgumentException($"{day} is not a puzzle day!", nameof(day))
		};
	}
}
