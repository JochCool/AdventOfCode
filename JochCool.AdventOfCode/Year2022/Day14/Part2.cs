namespace JochCool.AdventOfCode.Year2022.Day14;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		// highest coordinate, from just looking through the file. One extra row is added at the bottom.
		//*
		const int maxY = 173;
		/*/
		const int maxY = 10;
		//*/
		const int minX = 500 - maxY;
		const int maxX = 500 + maxY;

		const int gridWidth = maxX - minX + 1;
		const int gridHeight = maxY + 1;

		// grid of (x,y)
		bool[,] grid = new bool[gridWidth, gridHeight];

		int posX = 0, posY = 0;

		foreach (string line in inputReader.ReadLines())
		{
			int numStart = 0;
			while (true)
			{
				int numEnd = line.IndexOf(',', numStart);
				int x = int.Parse(line.AsSpan(numStart, numEnd - numStart));

				numStart = numEnd + 1;
				numEnd = line.IndexOf(' ', numStart);
				if (numEnd == -1) numEnd = line.Length;
				int y = int.Parse(line.AsSpan(numStart, numEnd - numStart));

				if (posX == 0)
				{
					grid[x - minX, y] = true;
					posX = x;
					posY = y;
				}
				else if (x == posX)
				{
					while (posY != y)
					{
						if (posY < y) posY++; else posY--;
						grid[posX - minX, posY] = true;
					}
				}
				else if (y == posY)
				{
					while (posX != x)
					{
						if (posX < x) posX++; else posX--;
						grid[posX - minX, posY] = true;
					}
				}
				else
				{
					Console.WriteLine($"This line is not straight (around char {numStart}):\n{line}");
				}

				if (numEnd == line.Length) break;
				numStart = numEnd + 4;
			}
			posX = 0;
			posY = 0;
		}

		PrintGrid(grid, gridWidth, gridHeight);

		int numUnitsAtRest = 0;
		Stack<Position> path = new();
		Position sandUnit = new(500 - minX, 0);

		while (true)
		{
			Position prevPos = sandUnit;

			sandUnit.Y++;
			if (sandUnit.Y >= gridHeight) goto StopThisUnit;

			if (!grid[sandUnit.X, sandUnit.Y]) { } // can fall straight down; don't modify X
			else if (!grid[sandUnit.X - 1, sandUnit.Y]) sandUnit.X--;
			else if (!grid[sandUnit.X + 1, sandUnit.Y]) sandUnit.X++;
			else goto StopThisUnit;

			path.Push(prevPos);
			continue;

		StopThisUnit:
			// Found the bottom; mark it as filled
			grid[sandUnit.X, sandUnit.Y - 1] = true;
			numUnitsAtRest++;

			if (path.Count == 0)
			{
				Console.WriteLine($"Ran out of space for sand. Last unit fell at {sandUnit.X}, {sandUnit.Y - 1}. {numUnitsAtRest} units have fallen.");
				break;
			}
			sandUnit = path.Pop();
		}

		PrintGrid(grid, gridWidth, gridHeight);
		return numUnitsAtRest.ToInvariantString();
	}

	private static void PrintGrid(bool[,] grid, int width, int height)
	{
		StringBuilder gridSb = new();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				gridSb.Append(grid[x, y] ? '#' : '.');
			}
			gridSb.Append('\n');
		}
		Console.WriteLine(gridSb.ToString());
	}
}
