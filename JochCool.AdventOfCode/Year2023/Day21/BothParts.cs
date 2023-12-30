namespace JochCool.AdventOfCode.Year2023.Day21;

static class BothParts
{
	public static int CountSteps(string[] grid, Vector<int> startPos, int maxStepCount)
	{
		if (maxStepCount < 0) return 0;

		Vector<int> gridSize = new(grid[0].Length, grid.Length);
		BitGrid visited = new(gridSize);

		List<Vector<int>> toVisit = [startPos];
		visited[startPos] = true;

		// 0 if maxStepCount is odd, 1 if it's even (because then we need to also count the starting position).
		int result = ~maxStepCount & 1;

		Vector<int>[] directions = Vector<int>.AxisUnitVectors;
		for (int i = 0; i < maxStepCount; i++)
		{
			List<Vector<int>> newToVisit = new();
			foreach (Vector<int> pos in toVisit)
			{
				foreach (Vector<int> direction in directions)
				{
					Vector<int> newPos = pos + direction;
					if (!newPos.IsInBox(Vector<int>.Origin, new(grid[0].Length - 1, grid.Length - 1))) continue;
					if (visited[newPos]) continue;
					if (grid[newPos.Y][newPos.X] == '#')
					{
						continue;
					}

					visited[newPos] = true;
					newToVisit.Add(newPos);

					// when we've taken 1 step, i = 0, so the parity is opposite
					if ((i & 1) != (maxStepCount & 1))
					{
						//Console.WriteLine(newPos);
						result++;
					}
				}
			}
			toVisit = newToVisit;

			if (toVisit.Count == 0) break;
		}

		//Console.WriteLine($"From {startPos}, {maxStepCount} steps: {result}");
		//Console.WriteLine(visited.ToString());
		return result;
	}
}
