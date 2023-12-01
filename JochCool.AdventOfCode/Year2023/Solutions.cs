namespace JochCool.AdventOfCode.Year2023;

public static class Solutions
{
	public static (Solution Part1, Solution Part2) Get(int day)
	{
		return day switch
		{
			1 => (Day01.Part1.Solve, Day01.Part2.Solve),
			> 1 and <= 25 => throw new NotImplementedException("This puzzle is yet to be solved."),
			_ => throw new ArgumentException($"{day} is not a puzzle day!", nameof(day))
		};
	}
}
