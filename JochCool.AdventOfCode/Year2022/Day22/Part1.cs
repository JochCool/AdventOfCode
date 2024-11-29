namespace JochCool.AdventOfCode.Year2022.Day22;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();
		string path = grid[^1];

		Vector<int> pos = new(grid[0].IndexOf('.'), 0);

		Vector<int>[] directions =
		[
			Vector<int>.ToPositiveX,
			Vector<int>.ToPositiveY,
			Vector<int>.ToNegativeX,
			Vector<int>.ToNegativeY
		];

		int pathI = 0;
		int dirI = 0;
		while (pathI < path.Length)
		{
			int numberEndI = pathI;
			while (path[numberEndI] is >= '0' and <= '9')
			{
				numberEndI++;
				if (numberEndI >= path.Length) break;
			}

			if (numberEndI != pathI)
			{
				int toMove = int.Parse(path.AsSpan(pathI, numberEndI - pathI));

				Vector<int> dir = directions[dirI];
				while (toMove --> 0)
				{
					Vector<int> newPos = pos + dir;
					char square;
					if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= grid[newPos.Y].Length || (square = grid[newPos.Y][newPos.X]) == ' ')
					{
						newPos = pos - dir;
						while (newPos.X >= 0 && newPos.Y >= 0 && newPos.X < grid[newPos.Y].Length && grid[newPos.Y][newPos.X] != ' ')
						{
							newPos -= dir;
						}
						newPos += dir;
						square = grid[newPos.Y][newPos.X];
					}
					if (square == '#')
					{
						break;
					}
					else if (square == '.')
					{
						pos = newPos;
					}
					else throw new InvalidDataException($"Unknown character '{square}' in grid at line {newPos.Y} column {newPos.X}.");
				}

				pathI = numberEndI;
			}
			else
			{
				switch (path[pathI])
				{
					case 'L':
					{
						dirI--;
						if (dirI == -1) dirI = directions.Length - 1;
						break;
					}
					case 'R':
					{
						dirI++;
						if (dirI == directions.Length) dirI = 0;
						break;
					}
					default:
					{
						throw new InvalidDataException($"Unknown character '{path[pathI]}' in path at position {pathI}.");
					}
				}
				pathI++;
			}
		}

		Console.WriteLine($"Landed at {pos} facing {dirI}");
		int result = 4 * (pos.X + 1) + 1000 * (pos.Y + 1) + dirI;

		return result.ToInvariantString();
	}
}
