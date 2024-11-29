namespace JochCool.AdventOfCode.Year2022.Day08;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		int count = grid.Length * 2 + grid[0].Length + grid[^1].Length - 4;

		for (int y = 1; y < grid.Length - 1; y++)
		{
			for (int x = 1; x < grid[y].Length - 1; x++)
			{
				char height = grid[y][x];
				for (int i = 0; ; i++)
				{
					if (i == x) goto Count;
					if (grid[y][i] >= height) break;
				}
				for (int i = 0; ; i++)
				{
					if (i == y) goto Count;
					if (grid[i][x] >= height) break;
				}
				for (int i = grid[y].Length - 1; ; i--)
				{
					if (i == x) goto Count;
					if (grid[y][i] >= height) break;
				}
				for (int i = grid.Length - 1; ; i--)
				{
					if (i == y) goto Count;
					if (grid[i][x] >= height) break;
				}
				continue;

			Count:
				count++;
			}
		}

		return count.ToInvariantString();

		/*
		IEnumerator<string> lines = inputReader.ReadLines().GetEnumerator();

		lines.MoveNext();
		string line = lines.Current;

		int result = line.Length * 2;
		char[] tallestPerClm = line.ToCharArray();

		Stack<char>[] toCountVertically = new Stack<char>[line.Length];
		for (int i = 0; i < line.Length; i++)
		{
			toCountVertically[i] = new();
		}

		lines.MoveNext();
		line = lines.Current;

		while (lines.MoveNext())
		{
			result += 2;
			char tallest = line[0];
			int tallestI = 0;
			for (int i = 1; i < line.Length - 1; i++)
			{
				char height = line[i];
				bool isVisible = false;
				if (height > tallest)
				{
					tallest = line[i];
					tallestI = i;
					isVisible = true;
				}
				if (height > tallestPerClm[i])
				{
					tallestPerClm[i] = line[i];
					toCountVertically[i].Clear();
					toCountVertically[i].Push(line[i]);
					isVisible = true;
				}
				else
				{
					while (height > toCountVertically[i].Peek())
					{
						toCountVertically[i].Pop();
					}
				}
				if (isVisible)
				{
					result++;
				}
				else if (toCountVertically[i].Count == 0 || toCountVertically[i].Peek() > height)
				{
					toCountVertically[i].Push(height);
				}
			}
			for (int i = line.Length - 1; i >= tallestI; i--)
			{
				if (line)
			}

			line = lines.Current;
		}

		// don't have to update result again because I already did *2 earlier

		for (int i = 0; i < line.Length - 1; i++)
		{
			while (line[i] > toCountVertically[i].Peek())
			{
				toCountVertically[i].Pop();
			}
			result += toCountVertically[i].Count;
		}
		*/
	}
}
