namespace JochCool.AdventOfCode.Year2022.Day09;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		const int size = 512;
		const int startPos = size / 2;

		int numVisited = 1;
		bool[,] visited = new bool[size, size];

		Vector<int> head = new(startPos, startPos);
		Vector<int> tail = new(startPos, startPos);
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
				head += direction;

				if (tail.X < head.X - 1)
				{
					tail.X = head.X - 1;
					tail.Y = head.Y;
				}
				else if (tail.X > head.X + 1)
				{
					tail.X = head.X + 1;
					tail.Y = head.Y;
				}
				else if (tail.Y < head.Y - 1)
				{
					tail.Y = head.Y - 1;
					tail.X = head.X;
				}
				else if (tail.Y > head.Y + 1)
				{
					tail.Y = head.Y + 1;
					tail.X = head.X;
				}
				if (!visited[tail.X, tail.Y])
				{
					visited[tail.X, tail.Y] = true;
					numVisited++;
				}
			}
		}

		return numVisited.ToString();
	}
}
