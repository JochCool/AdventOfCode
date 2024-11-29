namespace JochCool.AdventOfCode.Year2023.Day14;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		int sum = 0;
		for (int x = 0; x < grid[0].Length; x++)
		{
			int load = grid.Length;
			for (int y = 0; y < grid.Length; y++)
			{
				switch (grid[y][x])
				{
					case 'O':
					{
						sum += load;
						load--;
						break;
					}

					case '#':
					{
						load = grid.Length - y - 1;
						break;
					}
				}
			}
		}
		return sum.ToInvariantString();
	}
}
