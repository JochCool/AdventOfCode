namespace JochCool.AdventOfCode.Year2023.Day14;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		char[][] grid = inputReader.ReadLines().Select(line => line.ToCharArray()).ToArray();

		Vector<int> gridSize = new(grid[0].Length, grid.Length);
		Vector<int> gridSizeSwizzled = Vector<int>.Swizzle(gridSize);

		List<char[][]> pastGrids = [];

		while (true)
		{
			Tilt(grid, gridSize, v => v);
			Tilt(grid, gridSizeSwizzled, Vector<int>.Swizzle);
			Tilt(grid, gridSize, v => new(v.X, gridSize.Y - v.Y - 1));
			Tilt(grid, gridSizeSwizzled, v => new(gridSize.Y - v.Y - 1, v.X));

			for (int i = 0; i < pastGrids.Count; i++)
			{
				if (CollectionUtil.JaggedArraysEqual(pastGrids[i], grid))
				{
					Console.WriteLine($"{i} and {pastGrids.Count} are equal");
					CollectionUtil.PrintGrid(grid);
					Console.WriteLine();
					CollectionUtil.PrintGrid(pastGrids[i]);

					int patternLength = pastGrids.Count - i;
					int finalIndex = 1000000000 % patternLength + i - 1;
					if (finalIndex < i) finalIndex += patternLength;
					Console.WriteLine($"Final index: {finalIndex}");

					CollectionUtil.PrintGrid(pastGrids[finalIndex]);
					return GetLoad(pastGrids[finalIndex]).ToString();
				}
			}

			pastGrids.Add(CollectionUtil.CopyJaggedArray(grid));

			//Console.WriteLine($"{pastGrids.Count} cycles");
			//CollectionUtil.PrintGrid(grid);
			//Console.WriteLine();
		}
	}

	private static void Tilt(char[][] grid, Vector<int> gridSize, Func<Vector<int>, Vector<int>> coordTransformer)
	{
		for (int x = 0; x < gridSize.X; x++)
		{
			int targetY = 0;
			for (int y = 0; y < gridSize.Y; y++)
			{
				// TODO: this is an affine transformation, so the delegate can actually be replaced with a matrix
				Vector<int> transformedCoord = coordTransformer(new(x, y));
				switch (grid[transformedCoord.Y][transformedCoord.X])
				{
					case 'O':
					{
						grid[transformedCoord.Y][transformedCoord.X] = '.';

						Vector<int> transformedTargetCoord = coordTransformer(new(x, targetY));
						grid[transformedTargetCoord.Y][transformedTargetCoord.X] = 'O';

						targetY++;
						break;
					}

					case '#':
					{
						targetY = y + 1;
						break;
					}
				}
			}
		}
	}

	private static int GetLoad(char[][] grid)
	{
		int sum = 0;
		for (int x = 0; x < grid[0].Length; x++)
		{
			for (int y = 0; y < grid.Length; y++)
			{
				if (grid[y][x] == 'O')
				{
					sum += grid.Length - y;
				}
			}
		}
		return sum;
	}
}
