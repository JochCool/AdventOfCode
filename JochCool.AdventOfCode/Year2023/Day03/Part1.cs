namespace JochCool.AdventOfCode.Year2023.Day03;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		int sum = 0;

		Vector<int>[] dirs = [
			new(-1, -1),
			new(-1, 0),
			new(-1, 1),
			new(0, -1),
			new(0, 1),
			new(1, -1),
			new(1, 0),
			new(1, 1)
		];

		char[][] grid = inputReader.ReadLines().Select(line => line.ToCharArray()).ToArray();

		Vector<int> size = new(grid.Length, grid[0].Length);

		for (int y = 0; y < grid.Length; y++)
		{
			for (int x = 0; x < grid[y].Length; x++)
			{
				if (grid[y][x] is not ('.' or >= '0' and <= '9'))
				{
					foreach (Vector<int> dir in dirs)
					{
						Vector<int> newPos = new Vector<int>(x, y) + dir;
						if (!newPos.IsInBox(new(0, 0), size)) continue;

						if (grid[newPos.Y][newPos.X] is >= '0' and <= '9')
						{
							char[] line = grid[newPos.Y];

							int startPos = newPos.X;
							while (startPos > 0 && line[startPos - 1] is >= '0' and <= '9')
								startPos--;

							int endPos = newPos.X;
							while (endPos < line.Length - 1 && line[endPos + 1] is >= '0' and <= '9')
								endPos++;

							int count = endPos - startPos + 1;

							int num = int.Parse(line.AsSpan(startPos, count));
							sum += num;

							Array.Fill(line, '.', startPos, count);
						}
					}
				}
			}
		}

		foreach (char[] line in grid)
		{
			Console.WriteLine(new string(line));
		}

		return sum.ToString();
	}
}
