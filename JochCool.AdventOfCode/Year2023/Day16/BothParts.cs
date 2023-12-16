namespace JochCool.AdventOfCode.Year2023.Day16;

static class BothParts
{
	public static int SendBeam(string[] grid, Beam beam)
	{
		Vector<int> gridSize = new Vector<int>(grid[0].Length, grid.Length);
		Vector<int> maxPos = gridSize + new Vector<int>(-1, -1);

		Stack<Beam> splitBeams = [];

		BitGrid visited = new(gridSize);
		int visitedCount = 0;

		while (true)
		{
			beam.Position += beam.Direction;

			if (!beam.Position.IsInBox(Vector<int>.Origin, maxPos))
			{
				if (splitBeams.TryPop(out beam))
					continue;
				return visitedCount;
			}

			bool visitedHere = visited[beam.Position];
			if (!visitedHere)
			{
				visited[beam.Position] = true;
				visitedCount++;
			}

			switch (grid[beam.Position.Y][beam.Position.X])
			{
				case '.':
					continue;

				case '/':
				{
					beam.Direction = -Vector<int>.Swizzle(beam.Direction);
					continue;
				}

				case '\\':
				{
					beam.Direction = Vector<int>.Swizzle(beam.Direction);
					continue;
				}

				case '-':
				{
					if (HandleSplitter(ref beam, splitBeams, Vector<int>.ToPositiveX, visitedHere))
						continue;
					return visitedCount;
				}

				case '|':
				{
					if (HandleSplitter(ref beam, splitBeams, Vector<int>.ToPositiveY, visitedHere))
						continue;
					return visitedCount;
				}

				default:
					throw new FormatException($"Unexpected character at {beam.Position}.");
			}
		}
	}

	private static bool HandleSplitter(ref Beam beam, Stack<Beam> splitBeams, Vector<int> splitDirection, bool visitedHere)
	{
		if (visitedHere)
		{
			return splitBeams.TryPop(out beam);
		}

		Vector<int> beamDir = beam.Direction;
		if (beamDir == splitDirection || beamDir == -splitDirection)
			return true;

		beam.Direction = splitDirection;

		Beam newSplitBeam = beam;
		newSplitBeam.Direction = -splitDirection;
		splitBeams.Push(newSplitBeam);
		return true;
	}
}
