namespace JochCool.AdventOfCode.Year2023.Day21;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();
		Vector<int> startPos = grid.PositionOf('S');
		return BothParts.CountSteps(grid, startPos, 64).ToString();
	}
}
