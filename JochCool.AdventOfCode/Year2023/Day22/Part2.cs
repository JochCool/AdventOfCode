namespace JochCool.AdventOfCode.Year2023.Day22;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		List<Brick> bricks = inputReader.ParseAllLines<Brick>();
		Console.WriteLine($"{bricks.Count} bricks");

		BothParts.LetBricksFall(bricks);

		int result = 0;
		foreach (Brick brick in bricks)
		{
			result += brick.GetTotalBeingSupported();
		}

		return result.ToInvariantString();
	}
}
