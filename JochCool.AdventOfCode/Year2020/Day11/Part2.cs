namespace JochCool.AdventOfCode.Year2020.Day11;

public static class Part2
{
	const char Empty = 'L';
	const char Occupied = '#';
	const char Floor = '.';

	// All directions in which ppl can look, as (row, column).
	static readonly (int, int)[] directions =
	[
		(-1, -1),
		(-1, 0),
		(-1, 1),
		(0, -1),
		(0, 1),
		(1, -1),
		(1, 0),
		(1, 1)
	];

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

					int numAdjacentOccupied = 0;
					foreach ((int, int) direction in directions)
					{
						// raycasting
						int lookingRow = row;
						int lookingClm = clm;
						while (true)
						{
							lookingRow += direction.Item1;
							lookingClm += direction.Item2;

							// out of bounds?
							if (lookingRow < 0 || lookingRow >= prevArea.Length || lookingClm < 0 || lookingClm >= prevArea[row].Length) break;
							
							// occoupied?
							if (prevArea[lookingRow][lookingClm] == Occupied)
							{
								numAdjacentOccupied++;
								break;
							}
							if (prevArea[lookingRow][lookingClm] != Floor) break;
						}
					}

					// Change
					if (numAdjacentOccupied == 0)
					{
						newArea[row][clm] = Occupied;
						Console.Write(Occupied);
					}
					else if (numAdjacentOccupied >= 5)
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

		return totalOccupied.ToInvariantString();
	}
}
