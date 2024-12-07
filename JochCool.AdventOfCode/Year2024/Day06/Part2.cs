namespace JochCool.AdventOfCode.Year2024.Day06;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		char[,] grid = inputReader.ReadCharGrid(defaultChar: '.');

		Vector<int> gridSize = new(grid.GetLength(1), grid.GetLength(0));

		Vector<int> startPos = grid.PositionOf('^');

		int count = 0;

		// try everything
		for (int y = 0; y < gridSize.Y; y++)
		{
			for (int x = 0; x < gridSize.X; x++)
			{
				if (grid[y, x] == '.')
				{
					grid[y, x] = 'O';

					if (TestForLoop(grid, startPos, Vector<int>.ToNegativeY))
					{
						count++;
					}

					grid[y, x] = '.';
				}
			}
		}

		return count.ToInvariantString();
	}

	private static bool TestForLoop(char[,] grid, Vector<int> pos, Vector<int> dir)
	{
		HashSet<(Vector<int> Pos, Vector<int> Dir)> visited = [(pos, dir)];

		while (true)
		{
			Vector<int> newPos = pos + dir;

			if (!grid.IsInBounds(newPos))
			{
				return false;
			}

			while (grid[newPos.Y, newPos.X] is '#' or 'O')
			{
				(dir.X, dir.Y) = (-dir.Y, dir.X);
				newPos = pos + dir;
			}

			pos = newPos;

			if (!visited.Add((pos, dir)))
			{
				return true;
			}
		}
	}
}
