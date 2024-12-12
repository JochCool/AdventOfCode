namespace JochCool.AdventOfCode.Year2024.Day10;

public static class BothParts
{
	public static string? Solve(TextReader inputReader, bool useRating)
	{
		char[,] grid = inputReader.ReadCharGrid();

		ReadOnlySpan<Vector<int>> directions = Vector<int>.AxisUnitVectors;

		// Keeps track of positions in the grid reachable from a trailhead.
		// Works like a queue with a fixed capacity, which is the theoretical maximum number of reachable positions with range 9.
		Span<Vector<int>> visited = new Vector<int>[10 * 10 + 9 * 9 - 1];

		int result = 0;

		for (int y = 0; y < grid.GetLength(0); y++)
		{
			for (int x = 0; x < grid.GetLength(1); x++)
			{
				if (grid[y, x] != '0')
				{
					continue;
				}

				ReadOnlySpan<Vector<int>> prevVisited = [new(x, y)];
				Span<Vector<int>> nextVisited = visited;

				for (char target = '1'; target <= '9'; target++)
				{
					// Add all positions that neighbour prevVisited and have the target height, to nextVisited.
					int nextVisitedCount = 0;
					foreach (Vector<int> position in prevVisited)
					{
						foreach (Vector<int> direction in directions)
						{
							Vector<int> newPos = position + direction;
							if (!grid.IsInBounds(newPos)) continue;
							if (grid[newPos.Y, newPos.X] != target) continue;

							// For trailhead scores, we should not count the same location double.
							// For trailhead ratings, we should count all possible paths.
							if (!useRating && nextVisited[..nextVisitedCount].Contains(newPos)) continue;

							nextVisited[nextVisitedCount] = newPos;
							nextVisitedCount++;
						}
					}

					prevVisited = nextVisited[..nextVisitedCount];
					nextVisited = nextVisited[nextVisitedCount..];
				}

				//Console.WriteLine(prevVisited.Length);
				result += prevVisited.Length;
			}
		}

		return result.ToInvariantString();
	}
}
