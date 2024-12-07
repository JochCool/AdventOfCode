namespace JochCool.AdventOfCode.Year2024.Day06;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		char[,] grid = inputReader.ReadCharGrid(defaultChar: '.');

		Vector<int> pos = grid.PositionOf('^');

		grid[pos.Y, pos.X] = 'X';

		int count = 1;
		Vector<int> dir = Vector<int>.ToNegativeY;

		while (true)
		{
			//Console.WriteLine(pos);
			Vector<int> newPos = pos + dir;

			if (!grid.IsInBounds(newPos))
			{
				break;
			}

			if (grid[newPos.Y, newPos.X] == '#')
			{
				(dir.X, dir.Y) = (-dir.Y, dir.X);
				newPos = pos + dir;
			}

			pos = newPos;

			if (grid[pos.Y, pos.X] != 'X')
			{
				count++;
			}
			else
			{
				grid[pos.Y, pos.X] = 'X';
			}

			//CollectionUtil.PrintGrid(grid);
		}

		return count.ToInvariantString();
	}
}
