namespace JochCool.AdventOfCode.Year2022.Day08;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		int highestScore = -1;

		for (int y = 0; y < grid.Length; y++)
		{
			for (int x = 0; x < grid[y].Length; x++)
			{
				char height = grid[y][x];

				int count = 0;
				for (int i = x - 1; i >= 0; i--)
				{
					count++;
					if (grid[y][i] >= height) break;
				}
				int total = count;

				count = 0;
				for (int i = y - 1; i >= 0; i--)
				{
					count++;
					if (grid[i][x] >= height) break;
				}
				total *= count;

				count = 0;
				for (int i = x + 1; i < grid[y].Length; i++)
				{
					count++;
					if (grid[y][i] >= height) break;
				}
				total *= count;

				count = 0;
				for (int i = y + 1; i < grid.Length; i++)
				{
					count++;
					if (grid[i][x] >= height) break;
				}
				total *= count;

				if (total > highestScore)
				{
					Console.WriteLine($"Found new highest of {total} at ({x}, {y})");
					highestScore = total;
				}
			}
		}

		return highestScore.ToInvariantString();
	}
}
