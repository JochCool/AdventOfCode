namespace JochCool.AdventOfCode.Year2024.Day05;

class Page
{
	public int Number { get; }
	
	public HashSet<int> Before { get; } = [];
	public HashSet<int> After { get; } = [];

	public Page(int number) => Number = number;

	internal void ThrowIfInvalid()
	{
		if (Before.Overlaps(After))
		{
			throw new ArgumentException("Invalid comparison graph.");
		}
	}
}
