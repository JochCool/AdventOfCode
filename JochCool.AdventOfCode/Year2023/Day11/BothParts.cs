namespace JochCool.AdventOfCode.Year2023.Day11;

static class BothParts
{
	public static string? Solve(TextReader inputReader, long spaceExpansion)
	{
		string[] grid = inputReader.ReadAllLines();

		HashSet<int> emptyRows = [];
		HashSet<int> emptyColumns = [];
		List<Vector<int>> galaxyPositions = [];

		for (int y = 0; y < grid.Length; y++)
		{
			if (grid[y].All(c => c != '#'))
			{
				emptyRows.Add(y);
			}
		}
		for (int x = 0; x < grid[0].Length; x++)
		{
			bool allEmpty = true;
			for (int y = 0; y < grid.Length; y++)
			{
				if (grid[y][x] == '#')
				{
					galaxyPositions.Add(new(x, y));
					allEmpty = false;
				}
			}
			if (allEmpty)
				emptyColumns.Add(x);
		}

		long sum = 0;
		for (int i1 = 0; i1 < galaxyPositions.Count; i1++)
		{
			for (int i2 = 0; i2 < galaxyPositions.Count; i2++)
			{
				if (i1 == i2) continue;
				Vector<int> pos1 = galaxyPositions[i1];
				Vector<int> pos2 = galaxyPositions[i2];

				long xDist = GetDimensionDist(pos1.X, pos2.X, emptyColumns, spaceExpansion);
				long yDist = GetDimensionDist(pos1.Y, pos2.Y, emptyRows, spaceExpansion);
				long totalDist = xDist + yDist;
				//Console.WriteLine($"Between {i1} and {i2}: {xDist} + {yDist} = {totalDist}");
				sum += totalDist;
			}
		}
		return (sum / 2).ToInvariantString();
	}

	// gets the distance in either x or y direction, in expanded space
	private static long GetDimensionDist(int value1, int value2, HashSet<int> emptyIndices, long spaceExpansion)
	{
		int biggest = int.Max(value1, value2);
		int smallest = int.Min(value1, value2);
		long dist = biggest - smallest;
		for (int x = smallest + 1; x < biggest; x++)
		{
			if (emptyIndices.Contains(x))
			{
				dist += spaceExpansion;
			}
		}
		return dist;
	}
}
