namespace JochCool.AdventOfCode.Year2022.Day12;

public static class Part1
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		Vector<int> gridSize = new(lines[0].Length, lines.Length);

		int[,] grid = new int[gridSize.Y, gridSize.X];
		int[,] numSteps = new int[gridSize.Y, gridSize.X];

		Vector<int> pos = new(-1, -1);
		Vector<int> target = new(-1, -1);

		for (int i = 0; i < gridSize.Y; i++)
		{
			for (int charI = 0; charI < lines[i].Length; charI++)
			{
				char @char = lines[i][charI];
				switch (@char)
				{
					case 'S':
					{
						pos = new(charI, i);
						numSteps[i, charI] = 1;
						grid[i, charI] = 0;
						break;
					}

					case 'E':
					{
						target = new(charI, i);
						grid[i, charI] = 'z' - 'a';
						break;
					}

					default:
					{
						grid[i, charI] = @char - 'a';
						break;
					}
				}
			}
		}

		if (pos == new Vector<int>(-1, -1))
		{
			Console.WriteLine("Starting position not found.");
			return null;
		}
		if (target == new Vector<int>(-1, -1))
		{
			Console.WriteLine("End position not found.");
			return null;
		}

		Vector<int>[] dirs =
		[
			Vector<int>.ToPositiveY,
			Vector<int>.ToPositiveX,
			Vector<int>.ToNegativeY,
			Vector<int>.ToNegativeX
		];

		PriorityQueue<Vector<int>, AStarPriority> toVisit = new();

		Vector<int> maxCoord = gridSize - new Vector<int>(1, 1);
		int stepsDone = 0;

		while (true)
		{
			int elevation = grid[pos.Y, pos.X];
			int nextSteps = numSteps[pos.Y, pos.X] + 1;
			foreach (Vector<int> dir in dirs)
			{
				Vector<int> newVector = pos + dir;
				if (!newVector.IsInBox(Vector<int>.Origin, maxCoord))
				{
					continue;
				}
				if (numSteps[newVector.Y, newVector.X] != 0 && numSteps[newVector.Y, newVector.X] <= nextSteps)
				{
					continue;
				}

				int elevationDiff = grid[newVector.Y, newVector.X] - elevation;
				if (elevationDiff > 1) continue;
				
				if (newVector == target)
				{
					Console.WriteLine($"Found after {stepsDone} steps!");
					Console.WriteLine(nextSteps - 1);
					PrintGrid(numSteps, gridSize);
					return (nextSteps - 1).ToString();
				}

				numSteps[newVector.Y, newVector.X] = nextSteps;
				toVisit.Enqueue(newVector, new AStarPriority(nextSteps, (newVector-target).TaxicabMagnitude));
			}

			stepsDone++;

			AStarPriority priority;
			do
			{
				if (!toVisit.TryDequeue(out pos, out priority))
				{
					Console.WriteLine("There is no path.");
					PrintGrid(numSteps, gridSize);
					return null;
				}
			}
			while (priority.Cost != numSteps[pos.Y, pos.X]); // This would mean we must have been here before
		}
	}

	static void PrintGrid(int[,] numSteps, Vector<int> gridSize)
	{
		StringBuilder sb = new();
		for (int row = 0; row < gridSize.Y; row++)
		{
			sb.Append(string.Format("{0,2}", row));
			sb.Append('.');
			for (int clm = 0; clm < gridSize.X; clm++)
			{
				sb.Append(' ');
				sb.Append(string.Format("{0,3}", numSteps[row, clm]));
			}
			sb.Append('\n');
		}
		Console.WriteLine(sb.ToString());
	}
}
