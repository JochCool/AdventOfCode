namespace JochCool.AdventOfCode.Year2023.Day10;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		Vector<int> startPos = grid.PositionOf('S');
		Console.WriteLine($"Start pos: {startPos}");

		foreach (Vector<int> startDir in Vector<int>.AxisUnitVectors)
		{
			Console.WriteLine($"-- Going to {startDir}");
			int numSteps = 0;
			bool success = BothParts.FollowPipes(grid, startPos, startDir, (_, _, _) =>
			{
				numSteps++;
			});
			if (success)
			{
				return (numSteps / 2).ToInvariantString();
			}
		}
		return null;
	}
}
