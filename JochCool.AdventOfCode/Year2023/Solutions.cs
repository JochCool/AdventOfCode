namespace JochCool.AdventOfCode.Year2023;

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
			> 5 and <= 25 => throw new NotImplementedException("This puzzle is yet to be solved."),
			_ => throw new ArgumentException($"{day} is not a puzzle day!", nameof(day))
		};
	}
}
