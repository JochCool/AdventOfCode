using System.Diagnostics;

namespace JochCool.AdventOfCode.Year2023.Day10;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		string[] grid = inputReader.ReadAllLines();

		Vector<int> startPos = grid.PositionOf('S');
		Console.WriteLine($"Start pos: {startPos}");

		Vector<int> size = new(grid[^1].Length, grid.Length);

		Tile[][] fillGrid = CollectionUtil.CreateJaggedArray<Tile>(size);

		foreach (Vector<int> startDir in Vector<int>.AxisUnitVectors)
		{
			Console.WriteLine($"-- Going to {startDir}");

			// We need to know whether, as we walk across the loop in step 2, to which side the loop is: our left or our right.
			// This can be determined by keeping track of how often we turn left or right.
			int numLeftTurns = 0;
			int numRightTurns = 0;

			// Step 1: find the loop
			bool success = BothParts.FollowPipes(grid, startPos, startDir, (Vector<int> pos, Vector<int> incomingDirection, Vector<int> outgoingDirection) =>
			{
				fillGrid[pos.Y][pos.X] = Tile.Pipe;

				if (incomingDirection.X == 0 && outgoingDirection.X != 0)
				{
					if (int.IsPositive(incomingDirection.Y) == int.IsPositive(outgoingDirection.X))
						numLeftTurns++;
					else
						numRightTurns++;
				}
				else if (incomingDirection.Y == 0 && outgoingDirection.Y != 0)
				{
					if (int.IsPositive(incomingDirection.X) == int.IsPositive(outgoingDirection.Y))
						numRightTurns++;
					else
						numLeftTurns++;
				}
			});

			if (success)
			{
				// Step 2: fill the enclosed area
				bool isLeft = numLeftTurns > numRightTurns;
				int numFilled = 0;
				bool secondSuccess = BothParts.FollowPipes(grid, startPos, startDir, (Vector<int> pos, Vector<int> incomingDirection, Vector<int> outgoingDirection) =>
				{
					Debug.Assert(fillGrid[pos.Y][pos.X] == Tile.Pipe);

					numFilled += Fill(fillGrid, pos, Rotate(incomingDirection, isLeft));
					numFilled += Fill(fillGrid, pos, Rotate(outgoingDirection, isLeft));
				});
				Debug.Assert(secondSuccess);

				CollectionUtil.PrintGrid(fillGrid, tile => tile switch
				{
					Tile.Pipe => 'X',
					Tile.Enclosed => 'F',
					_ => '.'
				});

				return numFilled.ToString();
			}

			// reset
			foreach (Tile[] line in fillGrid)
				Array.Clear(line);
		}
		return null;
	}

	// Rotates a vector 90°
	private static Vector<int> Rotate(Vector<int> vector, bool left)
	{
		return left
			? new(vector.Y, -vector.X)
			: new(-vector.Y, vector.X);
	}

	// Fills a line with Tile.Enclosed, until we hit a pipe
	private static int Fill(Tile[][] fillGrid, Vector<int> startPos, Vector<int> direction)
	{
		int numFilled = 0;

		Vector<int> pos = startPos;
		Tile currentTile;
		do
		{
			pos += direction;
			currentTile = fillGrid[pos.Y][pos.X];
			if (currentTile == Tile.NotInArea)
			{
				numFilled++;
				fillGrid[pos.Y][pos.X] = Tile.Enclosed;
			}
		}
		while (currentTile != Tile.Pipe);

		return numFilled;
	}

	enum Tile
	{
		NotInArea,
		Enclosed,
		Pipe
	}
}
