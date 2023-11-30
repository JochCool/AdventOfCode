namespace JochCool.AdventOfCode.Year2022.Day12;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] lines = inputReader.ReadAllLines();

		Vector<int> gridSize = new(lines[0].Length, lines.Length);

		int[,] grid = new int[gridSize.Y, gridSize.X];
		int[,] numSteps = new int[gridSize.Y, gridSize.X];

		Vector<int> pos = new(-1, -1);

		for (int i = 0; i < gridSize.Y; i++)
		{
			for (int charI = 0; charI < lines[i].Length; charI++)
			{
				char @char = lines[i][charI];
				switch (@char)
				{
					case 'E':
					{
						pos = new(charI, i);
						numSteps[i, charI] = 1;
						grid[i, charI] = 'z' - 'a';
						break;
					}

					case 'S':
					{
						grid[i, charI] = 0;
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

		Vector<int>[] dirs =
		[
			Vector<int>.ToPositiveY,
			Vector<int>.ToPositiveX,
			Vector<int>.ToNegativeY,
			Vector<int>.ToNegativeX
		];

		PriorityQueue<Vector<int>, int> toVisit = new();

		int lowest = int.MaxValue;

		Vector<int> maxCoord = gridSize - new Vector<int>(1, 1);

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

				int newElevation = grid[newVector.Y, newVector.X];
				if (newElevation < elevation - 1) continue;

				if (newElevation == 0)
				{
					Console.WriteLine("Found!");
					Console.WriteLine(nextSteps);
					if (nextSteps < lowest) lowest = nextSteps;
				}

				numSteps[newVector.Y, newVector.X] = nextSteps;
				toVisit.Enqueue(newVector, 500 - newElevation);
			}

			try
			{
				pos = toVisit.Dequeue();
			}
			catch (Exception exception)
			{
				Console.WriteLine("Lowest: " + lowest);
				Console.WriteLine("Remember to subtract 1");
				Console.WriteLine(exception);
				PrintGrid(numSteps, gridSize);
				return (lowest - 1).ToString();
			}
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
