namespace JochCool.AdventOfCode.Year2022.Day23;

public static class Part2
{
	public static string? Solve(TextReader inputReader)
	{
		List<Elf> elves = [];
		Dictionary<Vector<int>, SquareState> grid = [];
		int y = 0;
		foreach (string line in inputReader.ReadLines())
		{
			for (int x = 0; x < line.Length; x++)
			{
				if (line[x] == '#')
				{
					Vector<int> pos = new(x, y);
					grid[pos] = SquareState.Elf;
					elves.Add(new Elf(pos));
				}
			}
			y++;
		}
		Console.WriteLine($"{elves.Count} elves");

		ShiftingArray<Vector<int>[]> directions = new([
			[new(-1, -1), new(0, -1), new(1, -1)],
			[new(-1, 1), new(0, 1), new(1, 1)],
			[new(-1, -1), new(-1, 0), new(-1, 1)],
			[new(1, -1), new(1, 0), new(1, 1)]

		]);

		int numRounds = 0;
		while (true)
		{
			// First half of the round
			foreach (Elf elf in elves)
			{
				Vector<int> pos = elf.Position;

				bool anyElves = false;
				Vector<int>[]? chosenDirections = null;
				foreach (Vector<int>[] directionSet in directions)
				{
					foreach (Vector<int> direction in directionSet)
					{
						if (grid.TryGetValue(pos + direction, out SquareState squareState) && squareState == SquareState.Elf)
						{
							anyElves = true;
							goto Continue;
						}
					}
					chosenDirections ??= directionSet;

				Continue:
					if (anyElves && chosenDirections is not null) goto ProposeMove;
				}
				if (!anyElves || chosenDirections is null)
				{
					//elf.TargetPosition = elf.Position; // should have already happened
					continue;
				}

			ProposeMove:
				Vector<int> targetPos = pos + chosenDirections[1];
				if (grid.ContainsKey(targetPos))
				{
					grid[targetPos] = SquareState.MoveTargetConflict;
				}
				else
				{
					elf.TargetPosition = targetPos;
					grid[targetPos] = SquareState.MoveTarget;
				}
			}

			// Second half of the round
			int numElvesMoved = 0;
			foreach (Elf elf in elves)
			{
				if (elf.Position == elf.TargetPosition) continue;
				if (grid[elf.TargetPosition] == SquareState.MoveTargetConflict)
				{
					grid.Remove(elf.TargetPosition);
					elf.TargetPosition = elf.Position;
					continue;
				}

				grid.Remove(elf.Position);
				grid[elf.TargetPosition] = SquareState.Elf;
				elf.Position = elf.TargetPosition;
				numElvesMoved++;
			}

			/*
			foreach (Elf elf in elves)
			{
				Console.WriteLine($"Elf at {elf.Position}");
			}
			Console.WriteLine();
			//*/

			directions.Shift();
			numRounds++;
			Console.WriteLine($"Round {numRounds}: {numElvesMoved} elves moved");
			if (numElvesMoved == 0) return numRounds.ToString();
		}
	}
}
