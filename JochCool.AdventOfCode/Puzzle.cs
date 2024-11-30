namespace JochCool.AdventOfCode;

public class Puzzle
{
	public int Year { get; }

	public int Day { get; }

	public int Part { get; }

	public Solution? Solution { get; }

	public Puzzle(int year, int dayNumber, int partNumber, Solution? solution = null)
	{
		Year = year;
		Day = dayNumber;
		Part = partNumber;
		Solution = solution;
	}

	public DateOnly Date => new DateOnly(Year, 12, Day);
}
