namespace JochCool.AdventOfCode.Year2022.Day09;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		const int size = 512;
		const int startPos = size / 2;
		const int numKnots = 10;

		int numVisited = 1;
		bool[,] visited = new bool[size, size];

		Vector<int>[] knots = new Vector<int>[numKnots];
		for (int i = 0; i < knots.Length; i++)
		{
			knots[i] = new(startPos, startPos);
		}

		visited[startPos, startPos] = true;

		Dictionary<char, Vector<int>> directions = new()
		{
			['R'] = new(1, 0),
			['U'] = new(0, -1),
			['L'] = new(-1, 0),
			['D'] = new(0, 1)
		};

		foreach (string line in inputReader.ReadLines())
		{
			Vector<int> direction = directions[line[0]];
			int dist = int.Parse(line.AsSpan(2));
			while (dist --> 0)
			{
				knots[0] += direction;

				for (int i = 1; i < numKnots; i++)
				{
					Vector<int> tail = knots[i];
					Vector<int> head = knots[i - 1];

					bool mustMove = Math.Abs(head.X - tail.X) >= 2 || Math.Abs(head.Y - tail.Y) >= 2;
					if (!mustMove) break;

					if (tail.X < head.X) tail.X++;
					else if (tail.X > head.X) tail.X--;
					if (tail.Y < head.Y) tail.Y++;
					else if (tail.Y > head.Y) tail.Y--;

					knots[i] = tail;
				}
				
				Vector<int> lastTail = knots[numKnots - 1];
				if (!visited[lastTail.X, lastTail.Y])
				{
					visited[lastTail.X, lastTail.Y] = true;
					numVisited++;
				}
			}
		}

		return numVisited.ToInvariantString();
	}
}
