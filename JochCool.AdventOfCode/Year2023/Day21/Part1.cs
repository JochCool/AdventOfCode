namespace JochCool.AdventOfCode.Year2023.Day21;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();
		BitGrid visited = new(new(grid[0].Length, grid.Length));

		int result = 0;
		Vector<int> startPos = grid.PositionOf('S');
		List<Vector<int>> toVisit = [startPos];
		List<Vector<int>> toVisitNext = [];

		Vector<int>[] directions = Vector<int>.AxisUnitVectors;
		for (int i = 0; i < 64; i++)
		{
			foreach (Vector<int> pos in toVisit)
			{
				foreach (Vector<int> direction in directions)
				{
					Vector<int> newPos = pos + direction;
					if (!newPos.IsInBox(Vector<int>.Origin, new(grid[0].Length-1, grid.Length-1))) continue;
					if (visited[newPos]) continue;
					if (grid[newPos.Y][newPos.X] == '#') continue;

					visited[newPos] = true;
					toVisitNext.Add(newPos);

					if (int.IsOddInteger(i))
					{
						//Console.WriteLine(newPos);
						result++;
					}
				}
			}
			(toVisit, toVisitNext) = (toVisitNext, toVisit);
			toVisitNext.Clear();
		}
		//Console.WriteLine(visited.ToString());
		return result.ToString();
	}
}
