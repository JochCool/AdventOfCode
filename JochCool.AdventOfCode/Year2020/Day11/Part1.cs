namespace JochCool.AdventOfCode.Year2020.Day11;

public static class Part1
{
	const char Empty = 'L';
	const char Occupied = '#';
	const char Floor = '.';

	public static string? Solve(TextReader inputReader)
	{
		List<char[]> input = inputReader.ParseAllLines(line => line.ToCharArray());

		char[][] prevArea = input.ToArray();
		char[][] newArea = prevArea.Select(line => line.ToArray()).ToArray(); // This makes sure that each individual array is copied.

		bool arraysAreDifferent;
		int totalOccupied;
		do
		{
			totalOccupied = 0;
			arraysAreDifferent = false;

			for (int row = 0; row < prevArea.Length; row++)
			{
				for (int clm = 0; clm < prevArea[row].Length; clm++)
				{
					if (prevArea[row][clm] == Floor)
					{
						Console.Write(Floor);
						continue;
					}

					// Find the num with a gazillion if statements
					int numAdjacentOccupied = 0;
					if (clm != 0 && prevArea[row][clm - 1] == Occupied) numAdjacentOccupied++;
					if (clm != prevArea[row].Length - 1 && prevArea[row][clm + 1] == Occupied) numAdjacentOccupied++;
					if (row != 0)
					{
						if (prevArea[row - 1][clm] == Occupied) numAdjacentOccupied++;
						if (clm != 0 && prevArea[row - 1][clm - 1] == Occupied) numAdjacentOccupied++;
						if (clm != prevArea[row - 1].Length - 1 && prevArea[row - 1][clm + 1] == Occupied) numAdjacentOccupied++;
					}
					if (row != prevArea.Length - 1)
					{
						if (prevArea[row + 1][clm] == Occupied) numAdjacentOccupied++;
						if (clm != 0 && prevArea[row + 1][clm - 1] == Occupied) numAdjacentOccupied++;
						if (clm != prevArea[row + 1].Length - 1 && prevArea[row + 1][clm + 1] == Occupied) numAdjacentOccupied++;
					}

					// Change
					if (numAdjacentOccupied == 0)
					{
						newArea[row][clm] = Occupied;
						Console.Write(Occupied);
					}
					else if (numAdjacentOccupied >= 4)
					{
						newArea[row][clm] = Empty;
						Console.Write(Empty);
					}
					else
					{
						newArea[row][clm] = prevArea[row][clm];
						Console.Write(prevArea[row][clm]);
					}

					// Check
					if (newArea[row][clm] == Occupied) totalOccupied++;
					if (newArea[row][clm] != prevArea[row][clm]) arraysAreDifferent = true;
				}
				Console.WriteLine();
			}

			// Swap
			char[][] tmp = prevArea;
			prevArea = newArea;
			newArea = tmp;

			Console.WriteLine($"Seats occupied: {totalOccupied}.");
			/*
			Console.WriteLine($"Seats occupied: {totalOccupied}. Press any key to continue.");
			_ = Console.ReadKey();
			Console.WriteLine();
			//*/

		} while (arraysAreDifferent);

		return totalOccupied.ToString();
	}
}
